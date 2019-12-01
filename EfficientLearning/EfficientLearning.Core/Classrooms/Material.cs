using EfLearning.Core.Announcements;
using EfLearning.Core.EntitiesHelper;
using System;

namespace EfLearning.Core.Classrooms
{
    public class Material : RootEntity<int>, ICreationTime
    {


        public int GivenClassroomId { get; set; }
        public virtual GivenClassroom GivenClassroom { get; set; }
        public MaterialType MaterialType { get; set; }
        public int MaterialScale { get; set; } // like out  of 100 || out of 4
        public string Question { get; set; }
        public string Hint { get; set; }

        public int AnnouncementId { get; set; }
        public virtual Announcement Announcement { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
