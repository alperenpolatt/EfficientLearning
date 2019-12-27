using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.Classroom
{
    public class MaterialResource
    {
        [Required]
        public int GivenClassroomId { get; set; }
        [Required]
        public MaterialType MaterialType { get; set; }
        public int MaterialScale { get; set; } // like out  of 100 || out of 4
        public string Question { get; set; }
        public string Hint { get; set; }
        public string Description { get; set; }//For Announcement
        public DateTime Deadline { get; set; }
    }
}
