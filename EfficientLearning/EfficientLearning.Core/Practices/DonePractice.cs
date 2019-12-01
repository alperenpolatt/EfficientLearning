using EfLearning.Core.EntitiesHelper;
using EfLearning.Core.Users;
using System;

namespace EfLearning.Core.Practices
{
    public class DonePractice : RootEntity<int>, ICreationTime
    {
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public int GivenPracticeId { get; set; }
        public virtual GivenPractice GivenPractice { get; set; }
        public int Point { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
