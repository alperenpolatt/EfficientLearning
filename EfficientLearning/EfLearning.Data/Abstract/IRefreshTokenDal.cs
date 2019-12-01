using EfLearning.Core.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Abstract
{
    public interface IRefreshTokenDal:IRepository<RefreshToken>
    {
        Task<RefreshToken> SingleAsync(string refreshToken);
    }
}
