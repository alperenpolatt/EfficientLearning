using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.Announcement
{
    public class CommentResource
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int AnnouncementId { get; set; }
        [Required]
        public int? UserId { get; set; }
    }
}
