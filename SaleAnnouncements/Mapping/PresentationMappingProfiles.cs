using AutoMapper;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.Models;

namespace SaleAnnouncements.Mapping
{
	public class PresentationMappingProfiles : Profile
	{
		public PresentationMappingProfiles()
		{
			CreateMap<OfferDto, OfferBindingModel>()
				.ForMember(x => x.Photos, opt => opt.Ignore());
			CreateMap<OfferBindingModel, OfferDto>()
				.ForMember(x => x.Photos, opt => opt.Ignore());
		}
	}
}
