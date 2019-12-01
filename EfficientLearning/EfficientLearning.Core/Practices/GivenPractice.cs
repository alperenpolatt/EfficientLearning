using EfLearning.Core.Classrooms;
using EfLearning.Core.EntitiesHelper;
using System.Collections.Generic;

namespace EfLearning.Core.Practices
{
    public class GivenPractice : RootEntity<int>
    {
        public int Level { get; set; }
        public string Definition { get; set; }
        public ProgrammingType ProgrammingType { get; set; }
        public string Question { get; set; }
        public string Solution { get; set; }
        public string Hint { get; set; }
        public virtual ICollection<DonePractice> DonePractices { get; set; }
    }
}
