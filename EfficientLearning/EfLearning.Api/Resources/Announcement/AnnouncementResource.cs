using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.Announcement
{
    public class AnnouncementResource
    {
        [Required]
        public int GivenClassroomId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
