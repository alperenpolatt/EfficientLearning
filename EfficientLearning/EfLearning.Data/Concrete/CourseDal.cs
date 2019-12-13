using EfLearning.Core.Classrooms;
using EfLearning.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Concrete
{
    public class CourseDal : BaseRepository<Course>, ICourseDal
    {
        public CourseDal(EfContext context) : base(context)
        {
        }

        public async Task<ICollection<IGrouping<ProgrammingType, Course>>> GetCoursesGroupByProgrammingType() { 
           return await _context.Courses.GroupBy(c => c.ProgrammingType).ToListAsync();
        }

    }
}
