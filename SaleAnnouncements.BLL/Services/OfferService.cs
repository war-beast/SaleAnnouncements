using AutoMapper;
using Microsoft.Extensions.Logging;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model;
using SaleAnnouncements.BLL.Model.Filters;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace SaleAnnouncements.BLL.Services
{
	public class OfferService : ServiceBase, IOfferService
	{
		#region private members

		private readonly IMapper _mapper;
		private readonly ILogger<OfferService> _logger;
		private readonly ICustomerService _customerService;
		private readonly IOfferStatusService _offerStatusService;
		private readonly ICategoryService _categoryService;

		#endregion

		#region constructor

		public OfferService(IUnitOfWork unitOfWork,
			IMapper mapper,
			ILogger<OfferService> logger,
			ICustomerService customerService,
			IOfferStatusService offerStatusService,
			ICategoryService categoryService) : base(unitOfWork)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
			_offerStatusService = offerStatusService ?? throw new ArgumentNullException(nameof(customerService));
			_categoryService = categoryService ?? throw new ArgumentNullException(nameof(customerService));
		}

		#endregion

		public async Task<IReadOnlyCollection<OfferDto>> GetByCategory(Guid categoryId)
		{
			//Список объявлений всегда будет отсортирован сперва по убыванию ценности (зависит от установленных статусов),
			//затем по убыванию даты обновления
			var offers = await Task.Run(() => _unitOfWork.Offers
				.GetAll()
				.Where(x => x.CategoryId == categoryId)
				.OrderByDescending(x => x.Sort)
				.ThenByDescending(x => x.UpdateDate));

			return _mapper.Map<List<OfferDto>>(offers.ToList());
		}

		public async Task<IReadOnlyCollection<OfferDto>> GetForUser(Guid customerId)
		{
			var offers = await Task.Run(() => _unitOfWork.Offers
				.GetAll()
				.Where(x => x.CustomerId == customerId)
				.OrderByDescending(x => x.CreationDate));


			return await GetOffersWithCategory(_mapper.Map<List<OfferDto>>(offers))
				.ToListAsync();
		}

		public async Task<Result> Create(OfferDto offer, string userEmail)
		{
			var result = new Result
			{
				IsSuccess = true
			};

			offer.CustomerId = await GetCurrentUserId(userEmail);

			try
			{
				result.EntityId = _unitOfWork.Offers.Create(_mapper.Map<Offer>(offer));

				//Сохраняем в БД маппинги объявление-статусы
				if (offer.SelectedStatusIds.Any())
					_offerStatusService.SetStatusForOffer(result.EntityId, offer.SelectedStatusIds);

				UpdateSortingParameter(offer);

				await _unitOfWork.SaveAsync();
			}
			catch (Exception exc)
			{
				var message = $"Не удалось сохранить объявлениe: {offer.Title}.";

				_logger.LogError(message);
				result = new Result
				{
					IsSuccess = false,
					Error = $"{message} {exc.Message}",
					EntityId = Guid.Empty
				};
			}

			return result;
		}

		public async Task<OfferDto> Get(Guid id) => 
			_mapper.Map<OfferDto>(await _unitOfWork.Offers.Get(id));

		public async Task<Result> Update(OfferDto offer)
		{
			var result = new Result
			{
				IsSuccess = true,
				EntityId = offer.Id ?? Guid.Empty
			};

			try
			{
				_unitOfWork.Offers.Update(_mapper.Map<Offer>(offer));
				await _unitOfWork.SaveAsync();
			}
			catch (Exception exc)
			{
				result = new Result
				{
					IsSuccess = false,
					Error = exc.Message,
					EntityId = Guid.Empty
				};
			}

			return result;
		}

		public async Task<Result> AddStatuses(Guid offerId, IEnumerable<Guid> statusIds)
		{
			var result = new Result
			{
				IsSuccess = true,
				EntityId = offerId
			};

			var statusIdsListing = statusIds.ToList();
			var offer = await Get(offerId);
			offer.SelectedStatusIds = statusIdsListing;

			try
			{
				_offerStatusService.SetStatusForOffer(offerId, statusIdsListing);
				UpdateSortingParameter(offer);
				await _unitOfWork.SaveAsync();
			}
			catch (Exception exc)
			{
				var message = $"Не удалось добавить статусы в объявлениe Id: {offerId}.";

				_logger.LogError(message);
				result = new Result
				{
					IsSuccess = false,
					Error = $"{message} {exc.Message}"
				};
			}

			return result;
		}

		public async Task<IReadOnlyCollection<OfferStatusDto>> GetOfferStatuses(Guid offerId)
		{
			var statuses = _unitOfWork.OffersStatusesMaps
				.GetAll()
				.Where(x => x.OfferId == offerId)
				.Select(x => x.Status);

			return await Task.Run(() => _mapper.Map<List<OfferStatusDto>>(statuses));
		}

		#region private methods

		private async Task<Guid> GetCurrentUserId(string email)
		{
			#region validation

			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentNullException(nameof(email));

			#endregion

			var customersFilter = new CustomerFilterBuilder()
				.SetEmail(email)
				.Build();
			var customer = await _customerService.GetFiltered(customersFilter);

			if (customer.Count == 0)
			{
				var message = $"Не удалось получить пользователя по Email: {email}";

				_logger.LogError(message);
				throw new InvalidOperationException(message);
			}

			return customer.First().Id!.Value;
		}

		private async IAsyncEnumerable<OfferDto> GetOffersWithCategory(IList<OfferDto> offers)
		{
			foreach (var offer in offers)
			{
				offer.Category = await _categoryService.Get(offer.CategoryId);
				offer.OffersStatuses = await _offerStatusService.GetOfferStatusMaps(offer.Id!.Value);
				yield return offer;
			}
		}

		private void UpdateSortingParameter(OfferDto offer)
		{
			var selectedStatusAmounts = _unitOfWork.OfferStatuses
				.GetAll()
				.Where(x => offer.SelectedStatusIds.Contains(x.Id))
				.Select(x => x.Amount)
				.ToList();

			//Корректируем величину, отвечающую за изменение позиции объявления в списках, в зависимости от установленных статусов
			foreach (var amount in selectedStatusAmounts)
			{
				offer.Sort += (int)amount;
				var coreOffer = _mapper.Map<Offer>(offer);
				_unitOfWork.Offers.Update(_mapper.Map<Offer>(offer));
			}
		}

		#endregion
	}
}
