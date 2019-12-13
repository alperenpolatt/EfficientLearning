using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Abstract
{
    public interface ICourseDal : IRepository<Course>
    {
        Task<ICollection<IGrouping<ProgrammingType,Course>>> GetCoursesGroupByProgrammingType();
    }
}
