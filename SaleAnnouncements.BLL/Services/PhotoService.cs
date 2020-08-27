using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SaleAnnouncements.DAL.Entities;

namespace SaleAnnouncements.BLL.Services
{
	public class PhotoService : ServiceBase, IPhotoService
	{
		#region private members

		private readonly IMapper _mapper;
		private readonly ILogger<PhotoService> _logger;

		#endregion

		#region constructor

		public PhotoService(IUnitOfWork unitOfWork, 
			IMapper mapper, 
			ILogger<PhotoService> logger) : base(unitOfWork)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		#endregion

		public async Task<PhotoDto> Get(Guid id) => 
			_mapper.Map<PhotoDto>(await _unitOfWork.Photos.Get(id));

		public async Task<IReadOnlyCollection<PhotoDto>> GetForOffer(Guid offerId)
		{
			var photos = _unitOfWork.Photos
				.GetAll()
				.Where(x => x.OfferId == offerId);

			return await Task.Run(() => _mapper.Map<List<PhotoDto>>(photos));
		}

		public async Task<Result> Save(IList<PhotoDto> photos, Guid offerId)
		{
			#region validation

			if (!photos.Any())
			{
				_logger.LogError($"Не получено ни одного фото для объявления Id: {offerId}");
				return new Result
				{
					IsSuccess = false,
					EntityId = offerId,
					Error = "Получен пустой список фотографий"
				};
			}

			#endregion

			var corePhotos = _mapper.Map<IEnumerable<Photo>>(photos);
			var result = new Result
			{
				IsSuccess = true,
				EntityId = offerId
			};

			var offer = await _unitOfWork.Offers.Get(offerId);
			var offerPhotos = offer.Photos.ToList();
			offerPhotos.AddRange(corePhotos);
			offer.Photos = offerPhotos;

			try
			{
				await _unitOfWork.SaveAsync();
			}
			catch (Exception exc)
			{
				var message = $"Не удалось сохранить фотографии для объявления Id: {offerId}";

				_logger.LogError(message);
				result = new Result
				{
					IsSuccess = false,
					EntityId = offerId,
					Error = $"{message} {exc.Message}",
				};
			}

			return result;
		}

		public async Task<Result> Delete(Guid id)
		{
			var result = new Result
			{
				IsSuccess = true,
				EntityId = id
			};

			await _unitOfWork.Photos.Delete(id);

			try
			{
				await _unitOfWork.SaveAsync();
			}
			catch (Exception)
			{
				var message = $"Не удалось удалить фотографию Id: {id}";

				_logger.LogError(message);
				result = new Result
				{
					IsSuccess = false,
					EntityId = id,
					Error = message
				};
			}

			return result;
		}
	}
}
