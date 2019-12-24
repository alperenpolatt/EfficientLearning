using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class TakenPointResponse
    {
        public int UserId { get; set; }
        public int Score { get; set; }
        public int GivenPracticeId { get; set; }
    }
}
