using EfLearning.Core.EntitiesHelper;
using EfLearning.Core.Users;
using System;

namespace EfLearning.Core.Classrooms
{
    public class MaterialAnswer : RootEntity<int>, ICreationTime
    {
        public string Answer { get; set; }

        public virtual AppUser User { get; set; }
        public int? UserId { get; set; }


        public DateTime CreationTime { get; set; }
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }
        public int Score { get; set; }

    }
}
