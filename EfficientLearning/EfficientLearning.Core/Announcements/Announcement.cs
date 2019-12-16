using EfLearning.Core.Classrooms;
using EfLearning.Core.EntitiesHelper;
using System;
using System.Collections.Generic;

namespace EfLearning.Core.Announcements
{
    public class Announcement : RootEntity<int>, ICreationTime
    {
        public virtual Material Material { get; set; }
        public int? MaterialId { get; set; }

        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
