using EfLearning.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Security
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(AppUser user);//Kullanıcı login olduğunda oluşturulacak
        void RevokeRefreshToken(AppUser user);//Kullanıcı logout olduğunda
    }
}
