using EfLearning.Core.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class UserListResponse : BaseResponse
    {
        public ICollection<AppUser> Users { get; set; }
        //success

        public UserListResponse(ICollection<AppUser> users) : base(true, String.Empty)
        {
            Users = users;
        }

        //fail

        public UserListResponse(string message) : base(false, message)
        {
            Users = null;
        }

    }
}
