﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.User
{
    public class UserWithRoleResource
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
