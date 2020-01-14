using EfLearning.Core.Announcements;
using EfLearning.Core.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class AnnouncementCommentResponse
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual ICollection<CommentResponse> Comments { get; set; }
    }
    public class AnnouncementResponse
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int AnnouncementId { get; set; }
        public DateTime CreationTime { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }
    }
}
