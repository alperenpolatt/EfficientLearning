using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class CoursePopularityResponse
    {
        public int Count { get; set; }
        public ProgrammingType ProgrammingType { get; set; }
    }
}
