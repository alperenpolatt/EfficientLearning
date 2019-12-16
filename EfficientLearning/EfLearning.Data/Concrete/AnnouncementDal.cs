using EfLearning.Core.Announcements;
using EfLearning.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Concrete
{
    public class AnnouncementDal : BaseRepository<Announcement>, IAnnouncementDal
    {
        public AnnouncementDal(EfContext context) : base(context)
        {
        }

        public async Task<ICollection<Announcement>> GetAllWithMaterialAndComments(int givenClassroomId)
        {
            return await _context.Announcements
                            .Where(a => a.Material.GivenClassroomId == givenClassroomId)
                            .Include(a => a.Material)
                            .Include(a => a.Comments)
                            .ToListAsync();
        }
    }
}
