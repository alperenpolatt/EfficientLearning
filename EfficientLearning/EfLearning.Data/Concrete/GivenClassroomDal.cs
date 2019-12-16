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
    public class GivenClassroomDal : BaseRepository<GivenClassroom>, IGivenClassroomDal
    {
        public GivenClassroomDal(EfContext context) : base(context)
        {
        }

        public async Task<GivenClassroom> GetByIdWithTakenClasroomsAndStudentsAsync(int id)
        {
            return await _context.GivenClassrooms
                            .Where(gc => gc.Id == id)
                            .Include(gc => gc.TakenClassrooms)
                            .ThenInclude(tc => tc.User)
                            .FirstOrDefaultAsync();
        }

        public async Task<ICollection<GivenClassroom>> GetBySearchTermAsync(string query)
        {
            return await _context.GivenClassrooms
                            .Where(gc => gc.Course.Name.Contains(query) || gc.Description.Contains(query))
                            .ToListAsync();
        }
    }
}
