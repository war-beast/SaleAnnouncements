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
using SaleAnnouncements.BLL.Extensions;
using SaleAnnouncements.DAL.Entities;

namespace SaleAnnouncements.BLL.Services
{
	public class MessageService : ServiceBase, IMessageService
	{
		#region private members

		private readonly IMapper _mapper;
		private readonly ILogger<MessageService> _logger;
		private readonly ICustomerService _customerService;

		#endregion

		public MessageService(IUnitOfWork unitOfWork,
			IMapper mapper,
			ILogger<MessageService> logger,
			ICustomerService customerService) : base(unitOfWork)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
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
					&& x.Subject.Equals(message.Subject)
					&& x.CustomerId == message.CustomerId);

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

		public async Task<IReadOnlyCollection<MessageTitle>> GetUserMessageTitles(Guid customerId)
		{
			//Находим все сообщения пользователя
			var userMessages = _unitOfWork.Messages
				.GetAll()
				.Where(x => x.CustomerId == customerId || x.CompanionId == customerId)
				.ToList();

			var childMessages = userMessages
				.Where(x => x.ParentId != null);

			//Находим сообщения, которые указаны в качестве родительских у найденных ранее
			var parentMessages = await GetMessagesParents(childMessages)
				.ToListAsync();

			//Объединяем списки и удаляем дубли
			var uniqueParentMessages = userMessages
				.Where(x => x.ParentId == null)
				.Union(parentMessages)
				.ToList()
				.DistinctBy(x => x.Id)
				.OrderByDescending(x => x.CreationDate);

			return await GetMessageTitles(uniqueParentMessages, customerId)
				.ToListAsync();
		}

		public async Task<MessageThread> GetMessageThread(Guid customerId, Guid parentMessageId)
		{
			var parentMessage = await _unitOfWork.Messages.Get(parentMessageId);
			var otherMessages = _unitOfWork.Messages
				.GetAll()
				.Where(x => x.ParentId == parentMessageId)
				.OrderBy(x => x.CreationDate)
				.ToList();
			otherMessages.Insert(0, parentMessage);

			return new MessageThread
			{
				Id = parentMessageId,
				Name = parentMessage.Subject,
				Messages = await GetMessagesWithCustomers(otherMessages, customerId)
					.ToListAsync(),
				CompanionId = parentMessage.CustomerId == customerId 
					? parentMessage.CompanionId 
					: parentMessage.CustomerId
			};
		}

		public async Task<Result> SaveMessageReply(MessageDto message)
		{
			var result = new Result
			{
				IsSuccess = true
			};

			var parentMessage = await _unitOfWork.Messages.Get(message.ParentId!.Value);
			message.Subject = parentMessage.Subject;

			try
			{
				_unitOfWork.Messages.Create(_mapper.Map<Message>(message));
				await _unitOfWork.SaveAsync();
			}
			catch (Exception exc)
			{
				var error = $"Не удалось сохранить ответ на сообщение Id: {message.ParentId}. {exc.Message}";

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

		#region ptivate methods

		private async IAsyncEnumerable<Message> GetMessagesParents(IEnumerable<Message> messages)
		{
			foreach (var message in messages)
			{
				yield return await _unitOfWork.Messages.Get(message.ParentId!.Value);
			}
		}

		private async IAsyncEnumerable<MessageTitle> GetMessageTitles(IEnumerable<Message> messages, Guid customerId)
		{
			foreach (var message in messages)
			{
				var customer = await _customerService.Get(message.CustomerId);
				var companion = await _customerService.Get(message.CompanionId);

				yield return new MessageTitle
				{
					Date = message.CreationDate.ToShortDateString(),
					Id = message.Id,
					Name = customerId == message.CustomerId 
						? companion.UserName 
						: customer.UserName
				};
			}
		}

		private async IAsyncEnumerable<SingleMessage> GetMessagesWithCustomers(IEnumerable<Message> messages, Guid customerId)
		{
			foreach (var message in messages)
			{
				var customer = await _customerService.Get(message.CustomerId);

				yield return new SingleMessage
				{
					Date = message.CreationDate.ToShortDateString(),
					Message = message.Description,
					Name = customerId == message.CustomerId
						? "Я"
						: customer.UserName,
					IsMyMessage = customerId == message.CustomerId
				};
			}
		}

		#endregion
	}
}
