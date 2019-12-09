using AutoMapper;
using EfLearning.Api.Resources;
using EfLearning.Core.Users;

namespace EfLearning.Api.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserResource, AppUser>();
            CreateMap<AppUser, UserResource>();

            CreateMap<UserUpdateResource, AppUser>();
            CreateMap<AppUser, UserUpdateResource>();
        }
    }
}
