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
    public class MaterialAnswerDal:BaseRepository<MaterialAnswer>,IMaterialAnswerDal
    {
        public MaterialAnswerDal(EfContext context):base(context)
        {

        }

        public async Task<MaterialAnswer> DeleteByCompositeKeysAsync(int userId, int materialId)
        {
            var entity = await _context.MaterialAnswers
                                .SingleOrDefaultAsync(ma => ma.UserId == userId && ma.MaterialId == materialId);
            base.Delete(entity);
            return entity;
        }

        public async Task<ICollection<MaterialAnswer>> GetByMaterialIdWithUsersAsync(int materialId)
        {
            return await _context.MaterialAnswers
                            .Where(ma=>ma.MaterialId==materialId)
                            .Include(ma => ma.User).ToListAsync();
        }

        public async Task<ICollection<MaterialAnswer>> GetWithMaterialAndUserAsync(int givenClassroomId)
        {
            return await _context.MaterialAnswers
                                .Where(ma => ma.Material.GivenClassroomId == givenClassroomId)
                                .Include(ma => ma.User)
                                .Include(ma => ma.Material)
                                .ToListAsync();

        }

        public async Task<MaterialAnswer> UpdateAsync(MaterialAnswer entity)
        {
            if (entity == null)
                return null;
            MaterialAnswer exist = await _context.MaterialAnswers
                .SingleOrDefaultAsync(ma => ma.UserId == entity.UserId &&
                ma.MaterialId == entity.MaterialId);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
            }
            return exist;
        }
    }
}
