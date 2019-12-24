using EfLearning.Core.EntitiesHelper;
using EfLearning.Core.Users;
using System;

namespace EfLearning.Core.Practices
{
    public class DonePractice :  ICreationTime
    {
        public virtual AppUser User { get; set; }
        public int UserId { get; set; }
        public int GivenPracticeId { get; set; }
        public virtual GivenPractice GivenPractice { get; set; }
        public int TotalDonePractice { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
