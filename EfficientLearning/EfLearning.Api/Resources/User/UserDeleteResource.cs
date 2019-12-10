using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.User
{
    public class UserDeleteResource
    {
        [Required]
        public int Id { get; set; }
    }
}
