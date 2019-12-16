using AutoMapper;
using EfLearning.Api.Resources.Announcement;
using EfLearning.Core.Announcements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Mapping
{
    public class CommentMapping:Profile
    {
        public CommentMapping()
        {
            CreateMap<CommentResource, Comment>();
            CreateMap<Comment, CommentResource>();
        }
    }
}
