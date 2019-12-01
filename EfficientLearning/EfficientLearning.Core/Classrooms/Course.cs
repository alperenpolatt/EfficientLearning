using EfLearning.Core.EntitiesHelper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfLearning.Core.Classrooms
{
    public class Course : RootEntity<int>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public ProgrammingType ProgrammingType { get; set; }
        public virtual ICollection<GivenClassroom> GivenClassrooms { get; set; }
    }
}
