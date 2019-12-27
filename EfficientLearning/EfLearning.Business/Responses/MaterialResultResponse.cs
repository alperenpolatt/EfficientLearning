using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class MaterialResultResponse
    {
        public int Id { get; set; }
        public MaterialType MaterialType { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreationTime { get; set; }


        public string Description { get; set; }//from announcement
        public int? Score { get; set; }//from MaterialAnswer
    }
}
