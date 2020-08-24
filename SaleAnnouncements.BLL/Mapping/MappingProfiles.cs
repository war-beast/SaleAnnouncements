using AutoMapper;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.DAL.Entities;

namespace SaleAnnouncements.BLL.Mapping
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<CategoryDto, Category>();
			CreateMap<Category, CategoryDto>();

			CreateMap<CustomerDto, Customer>();
			CreateMap<Customer, CustomerDto>();

			CreateMap<MessageDto, Message>();
			CreateMap<Message, MessageDto>();

			CreateMap<OfferDto, Offer>();
			CreateMap<Offer, OfferDto>();

			CreateMap<OfferStatusDto, OfferStatus>();
			CreateMap<OfferStatus, OfferStatusDto>();

			CreateMap<PhotoDto, Photo>();
			CreateMap<Photo, PhotoDto>();

			CreateMap<OfferStatusDto, OfferStatus>();
			CreateMap<OfferStatus, OfferStatusDto>();
		}
	}
}
