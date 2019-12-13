using EfLearning.Core.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class UserRoleResponse
    {
        public AppUser User { get; set; }
        public string Role { get; set; }
    }
}
