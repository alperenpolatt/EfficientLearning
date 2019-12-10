using EfLearning.Core.Announcements;
using EfLearning.Core.Classrooms;
using EfLearning.Core.EntitiesHelper;
using EfLearning.Core.Practices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EfLearning.Core.Users
{
    public class AppUser : IdentityUser<int>, ICreationTime
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual ICollection<TakenClassroom> TakenClassrooms { get; set; }
        public virtual ICollection<GivenClassroom> GivenClassrooms { get; set; }
        public virtual ICollection<DonePractice> DonePractices { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens{ get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<MaterialAnswer> MaterialAnswers { get; set; }
    }
}
