using AutoMapper;
using EfLearning.Business.Responses;
using EfLearning.Core.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Mapping
{
    class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<AppUser, SimpleUserResponse>();
        }
    }
}
