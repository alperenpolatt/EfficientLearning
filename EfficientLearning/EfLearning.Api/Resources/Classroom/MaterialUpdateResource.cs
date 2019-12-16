using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.Classroom
{
    public class MaterialUpdateResource
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int MaterialScale { get; set; } // like out  of 100 || out of 4
        [Required]
        public string Question { get; set; }
        [Required]
        public string Hint { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
