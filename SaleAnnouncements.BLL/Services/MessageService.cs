using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SaleAnnouncements.DAL.Entities;

namespace SaleAnnouncements.BLL.Services
{
	public class MessageService : ServiceBase, IMessageService
	{
		#region private members

		private readonly IMapper _mapper;
		private readonly ILogger<MessageService> _logger;

		#endregion

		public MessageService(IUnitOfWork unitOfWork, 
			IMapper mapper, 
			ILogger<MessageService> logger) : base(unitOfWork)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<Result> SaveMessage(MessageDto message)
		{
			var result = new Result
			{
				IsSuccess = true
			};

			var existingHostMessage = _unitOfWork.Messages
				.GetAll()
				.FirstOrDefault(x => x.ParentId == null 
					&& x.Subject.Equals(message.Subject));

			var coreMessage = _mapper.Map<Message>(message);
			if (existingHostMessage != null)
			{
				coreMessage.ParentId = existingHostMessage.Id;
			}

			try
			{
				result.EntityId = _unitOfWork.Messages.Create(coreMessage);
				await _unitOfWork.SaveAsync();
			}
			catch (Exception exc)
			{
				var error = $"Не удалось сохранить сообщение на объявление: {message.Subject}. {exc.Message}";

				_logger.LogError(error);
				result = new Result
				{
					IsSuccess = false,
					EntityId = Guid.Empty,
					Error = error
				};
			}

			return result;
		}
	}
}
