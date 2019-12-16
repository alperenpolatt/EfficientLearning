using AutoMapper;
using EfLearning.Business.Responses;
using EfLearning.Core.Announcements;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Mapping
{
    class AnnouncementMapping:Profile
    {
        public AnnouncementMapping()
        {
            CreateMap<Announcement, AnnouncementCommentResponse>();
            CreateMap<Announcement, AnnouncementResponse>();
            CreateMap<Comment, CommentResponse>();
        }
    }
}
