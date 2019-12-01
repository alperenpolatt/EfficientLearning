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
        public AppUser User { get; set; }
        public List<IdentityError> Errors { get; set; }
        private UserResponse(Boolean success, string message, AppUser user) : base(success, message)
        {
            this.User = user;
        }

        //success

        public UserResponse(AppUser user) : this(true, String.Empty, user)
        {
        }

        //fail

        public UserResponse(List<IdentityError> errors) : this(false, errors.FirstOrDefault().Description, null)
        {
        }
    }
}
