using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.Classroom
{
    public class GivenClassroomUpdateResource : IUpdatableResource
    {
        [Required]
        public int Id { get ; set ; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
