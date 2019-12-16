using EfLearning.Core.EntitiesHelper;
using EfLearning.Core.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfLearning.Core.Classrooms
{
    public class TakenClassroom :  ICreationTime
    {
        public virtual AppUser User { get; set; }
        public  int UserId { get; set; } //Student
        public int GivenClassroomId { get; set; }
        public virtual GivenClassroom GivenClassroom { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
