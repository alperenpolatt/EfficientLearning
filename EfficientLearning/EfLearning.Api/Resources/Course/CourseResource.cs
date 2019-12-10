using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Resources.Course
{
    public class CourseResource
    {
        public string Name { get; set; }
        public ProgrammingType ProgrammingType { get; set; }
    }
}
