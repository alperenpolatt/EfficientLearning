using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class DonePracticeNotificationResponse
    {
        public int GivenPracticeId { get; set; }
        public int Level { get; set; }
        public string Title { get; set; }
        public string ProgrammingType { get; set; }
    }
}
