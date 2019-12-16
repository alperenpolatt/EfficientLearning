﻿using EfLearning.Core.Announcements;
using EfLearning.Core.EntitiesHelper;
using EfLearning.Core.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfLearning.Core.Classrooms
{
    public class GivenClassroom : RootEntity<int>, ICreationTime, IPassivable
    {

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsActive { get; set; }



        public virtual AppUser User { get; set; }
        public int? UserId { get; set; } //Teacher


        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<TakenClassroom> TakenClassrooms { get; set; }
    }
}
