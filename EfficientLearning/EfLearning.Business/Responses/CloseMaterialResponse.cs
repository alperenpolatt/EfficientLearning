using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class CloseMaterialResponse
    {
        public int Id { get; set; }
        public int GivenClassroomId { get; set; }
        public string Description { get; set; }//from announcement
        public DateTime Deadline { get; set; }
        public DateTime CreationTime { get; set; }

        public string OwnMaterial { get; set; }
    }
}
