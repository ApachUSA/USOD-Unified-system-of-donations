using AutoMapper;
using Donor_Library.Entity;
using Donor_Library.ViewModel;

namespace USOD.DonorAPI.Services.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<Donor, DonorVM>()
				.ForMember(opt => opt.Donor_ID, opt => opt.MapFrom(src => src.Donor_ID))
				.ForMember(opt => opt.Surname, opt => opt.MapFrom(src => src.Surname))
				.ForMember(opt => opt.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(opt => opt.Patronymic, opt => opt.MapFrom(src => src.Patronymic))
				.ForMember(opt => opt.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(opt => opt.Username, opt => opt.MapFrom(src => src.Username))
				.ForMember(opt => opt.Logo, opt => opt.MapFrom(src => src.Logo))
				.ForMember(opt => opt.Media, opt => opt.MapFrom(src => src.Donor_Medias));

		}
	}
}
