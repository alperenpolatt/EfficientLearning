using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Abstract
{
    public interface IMaterialAnswerDal:IRepository<MaterialAnswer>
    {
        Task<MaterialAnswer> DeleteByCompositeKeysAsync(int userId, int materialId);
        Task<ICollection<MaterialAnswer>> GetByMaterialIdWithUsersAsync(int materialId);
        Task<ICollection<MaterialAnswer>> GetWithMaterialAndUserAsync(int givenClassroomId);
        Task<MaterialAnswer> UpdateAsync(MaterialAnswer entity);
    }
}
