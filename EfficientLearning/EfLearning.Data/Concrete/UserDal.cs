using EfLearning.Core.Users;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Data.Concrete
{
    public class UserDal : BaseRepository<AppUser>, IUserDal
    {
        public UserDal(EfContext context) : base(context)
        {
        }
    }
}
