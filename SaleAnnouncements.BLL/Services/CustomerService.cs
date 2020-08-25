using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;

namespace SaleAnnouncements.BLL.Services
{
	public class CustomerService : ServiceBase, ICustomerService
	{
		#region private members

		private readonly IMapper _mapper;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ILogger<CustomerService> _logger;

		#endregion

		#region constructor

		public CustomerService(IUnitOfWork unitOfWork,
			IMapper mapper, 
			UserManager<IdentityUser> userManager, 
			ILogger<CustomerService> logger) : base(unitOfWork)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_logger = logger;
		}

		#endregion

		public async Task<CustomerDto> Get(Guid id) => 
			_mapper.Map<CustomerDto>(await _unitOfWork.Customers.Get(id));

		public async Task<IReadOnlyCollection<CustomerDto>> GetFiltered(ICustomerFilter filter)
		{
			var customers = _unitOfWork.Customers
				.GetAll();

			if (filter.Ids.Any())
			{
				customers = customers.Where(x => filter.Ids.Contains(x.Id));
			}

			if (!string.IsNullOrWhiteSpace(filter.Email))
			{
				var user = await _userManager.FindByEmailAsync(filter.Email);
				if (user != null)
				{
					customers = customers.Where(x => x.UserId == user.Id);
				}
			}

			return _mapper.Map<List<CustomerDto>>(customers);
		}

		public async Task<Guid> Create(string userId)
		{
			var customer = new CustomerDto
			{
				UserId = userId
			};

			var newCustomerId =  await Task.Run(() => _unitOfWork.Customers.Create(_mapper.Map<Customer>(customer)));
			try
			{
				await _unitOfWork.SaveAsync();
			}
			catch (Exception exc)
			{
				var user = await _userManager.FindByIdAsync(userId);
				await _userManager.DeleteAsync(user);

				_logger.LogError($"Ошибка при создании нового пользователя Email: {user.Email}");
			}

			return newCustomerId;
		}
	}
}
