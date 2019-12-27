using EfLearning.Core.Classrooms;
using EfLearning.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Data.Concrete
{
    public class TakenClassroomDal : BaseRepository<TakenClassroom>, ITakenClassroomDal
    {
        public TakenClassroomDal(EfContext context) : base(context)
        {
        }

        public async Task<TakenClassroom> DeleteByCompositeKeysAsync(int userId, int givenClassroomId)
        {
            var entity = await _context.TakenClassrooms
                                .SingleOrDefaultAsync(tc => tc.UserId == userId && tc.GivenClassroomId == givenClassroomId);
            base.Delete(entity);
            return entity;

        }

        public async Task<ICollection<TakenClassroom>> GetByUserIdWithGivenClassroomAndCourseAsync(int userId)
        {
            return await _context.TakenClassrooms
                            .Where(tc => tc.UserId == userId)
                            .Include(tc => tc.GivenClassroom)
                            .ThenInclude(gc => gc.Course).ToListAsync();
                            
        }

        public async Task<ICollection<TakenClassroom>> GetByUserIdWithGivenClassroomAndItsMaterialsAsync(int userId)
        {
            return await _context.TakenClassrooms
                            .Where(tc => tc.UserId == userId)
                            .Include(tc => tc.GivenClassroom)
                            .ThenInclude(gc => gc.Materials)
                            .ThenInclude(mc=>mc.Announcement).ToListAsync();
        }
    }
}
