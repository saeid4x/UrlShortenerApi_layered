using UrlShortenerApi01.DTOs;
 
using UrlShortenerApi01.Models;
using Profile = AutoMapper.Profile;

namespace UrlShortenerApi01.Mappings{
    
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
                CreateMap<Membership, MembershipDto>().ReverseMap();
                CreateMap<UrlShortenerApi01.Models.Profile, ProfileDto>().ReverseMap();
                CreateMap<ShortLinks, ShortLinksDto>().ReverseMap();
                CreateMap<QrCode, QrCodeDto>().ReverseMap();
                CreateMap<ClickStat, ClickStatDto>().ReverseMap();
            }
        }
    
}
