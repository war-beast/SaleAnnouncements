using AutoMapper;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.Models;

namespace SaleAnnouncements.Mapping
{
	public class PresentationMappingProfiles : Profile
	{
		public PresentationMappingProfiles()
		{
			CreateMap<OfferDto, OfferBindingModel>();
			CreateMap<OfferBindingModel, OfferDto>();
		}
	}
}
