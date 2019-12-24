using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.Practice
{
    public class DonePracticeResource
    {
        
        [Required]
        public int GivenPracticeId { get; set; }
    }
}
