using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.Course
{
    public class CourseUpdateResource : CourseResource, IUpdatableResource
    {
        [Required]
        public int Id { get ; set ; }
    }
}
