using EfLearning.Core.Users;
using EfLearning.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Concrete
{
    public class RefreshTokenDal : BaseRepository<RefreshToken>, IRefreshTokenDal
    {
        public RefreshTokenDal(EfContext context) : base(context)
        {
        }

        public async Task<RefreshToken> SingleAsync(string refreshToken)
        {
            return await _context.RefreshTokens.SingleOrDefaultAsync(a => a.Token == refreshToken);
        }
    }
}
