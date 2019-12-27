using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class MaterialQuestionResponse
    {
        public MaterialType MaterialType { get; set; }
        public int? MaterialScale { get; set; } // like out  of 100 || out of 4
        public string Question { get; set; }
        public string Hint { get; set; }
        public string Description { get; set; } //from announcement
        public DateTime? Deadline { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
