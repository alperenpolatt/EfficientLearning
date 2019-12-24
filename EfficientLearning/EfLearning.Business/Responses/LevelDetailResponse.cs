using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class LevelDetailResponse
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Title { get; set; }
        public string Definition { get; set; }
        public string Question { get; set; }
    }
}
