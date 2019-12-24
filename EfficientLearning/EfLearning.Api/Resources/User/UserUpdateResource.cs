using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.User
{
    public class UserUpdateResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
      
    }
}
