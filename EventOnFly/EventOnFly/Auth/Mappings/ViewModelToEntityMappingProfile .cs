using EventOnFly.Web.Auth.ViewModels;
using EventOnFly.DataAccess.Data.DbModels;
using AutoMapper;

namespace EventOnFly.Web.Auth.Mappings
{
  public class ViewModelToEntityMappingProfile : Profile
  {
    public ViewModelToEntityMappingProfile()
    {
      CreateMap<RegistrationViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
    }
  }
}
