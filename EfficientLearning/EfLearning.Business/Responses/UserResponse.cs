using EfLearning.Core.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class UserResponse : BaseResponse
    {
        public AppUser user { get; set; }

        private UserResponse(bool success, string message, AppUser user) : base(success, message)
        {
            this.user = user;
        }

        //success

        public UserResponse(AppUser user) : this(true, String.Empty, user)
        {
        }

        //fail

        public UserResponse(string message) : this(false, message, null)
        {
        }

    }
}
