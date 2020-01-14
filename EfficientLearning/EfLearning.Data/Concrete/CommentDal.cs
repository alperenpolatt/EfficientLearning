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
    public class CommentDal:BaseRepository<Comment>,ICommentDal
    {
        public CommentDal(EfContext context) : base(context)
        {
        }

        public async Task<ICollection<Comment>> GetWithUserAsync(int announcementId)
        {
            return await _context.Comments
                            .Where(c => c.AnnouncementId == announcementId)
                            .Include(c => c.User).ToListAsync();
        }
    }
}
