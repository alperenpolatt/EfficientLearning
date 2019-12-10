using EfLearning.Core.Classrooms;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Data.Concrete
{
    public class CourseDal : BaseRepository<Course>, ICourseDal
    {
        public CourseDal(EfContext context) : base(context)
        {
        }
    }
}
