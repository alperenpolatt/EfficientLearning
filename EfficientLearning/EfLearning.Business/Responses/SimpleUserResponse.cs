using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class SimpleUserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
