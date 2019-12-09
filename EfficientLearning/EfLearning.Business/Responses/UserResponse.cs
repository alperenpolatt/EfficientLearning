using EfLearning.Core.Users;
using System;
namespace EfLearning.Business.Responses
{
    public class UserResponse : BaseResponse
    {
        public AppUser User { get; set; }
        //success

        public UserResponse(AppUser user) : base(true, String.Empty)
        {
            User = user;
        }

        //fail

        public UserResponse(string message) : base(false, message)
        {
            User = null;
        }

    }
}
