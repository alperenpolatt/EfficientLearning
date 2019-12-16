using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.Classroom
{
    public class TakenClassroomResource
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int GivenClassroomId { get; set; }
    }
}
