using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.Classroom
{
    public class MaterialAnswerResource
    {
        [Required]
        public string Answer { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int MaterialId { get; set; }
    }
}
