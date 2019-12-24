using EfLearning.Core.Practices;
using EfLearning.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Concrete
{
    public class DonePracticeDal : BaseRepository<DonePractice>, IDonePracticeDal
    {
        public DonePracticeDal(EfContext context) : base(context)
        {
        }

        public async Task<ICollection<DonePractice>> GetByUserIdWithGivenPracticeAsync(int userId)
        {
            return await _context.DonePractices
                           .Where(dp=>dp.UserId==userId)
                           .Include(dp => dp.GivenPractice)
                           .ToListAsync();
        }

        public async Task<DonePractice> UpdateAsync(DonePractice entity)
        {
                if (entity == null)
                    return null;
            DonePractice exist = await _context.DonePractices
                    .SingleOrDefaultAsync(dp => dp.UserId == entity.UserId &&
                    dp.GivenPracticeId == entity.GivenPracticeId);
                if (exist != null)
                {
                    _context.Entry(exist).CurrentValues.SetValues(entity);
                }
                return exist;
        }
    }
}
