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
    public class MaterialDal: BaseRepository<Material>, IMaterialDal
    {
        public MaterialDal(EfContext context) : base(context)
        {
        }

        public async Task<int> GetNumberOfMaterialAsync(int userId)
        {
            var all = await _context.Materials
                               .Include(m=>m.GivenClassroom)
                               .ThenInclude(gc=>gc.TakenClassrooms).ToListAsync();
            var count = all.Where(a => a.GivenClassroom.TakenClassrooms.Any(b => b.UserId == userId)).Count();
            return count;
        }

        public async Task<ICollection<Material>> GetWithAnnouncementAndAnswerAsync(int givenClasroomId)
        {
            var all= await _context.Materials
                            .Where(m => m.GivenClassroomId == givenClasroomId && (m.MaterialType==MaterialType.Question || m.MaterialType == MaterialType.Task) )
                            .Include(m => m.MaterialAnswers)
                            .Include(m => m.Announcement).ToListAsync();

            return all;
        }
    }
}
