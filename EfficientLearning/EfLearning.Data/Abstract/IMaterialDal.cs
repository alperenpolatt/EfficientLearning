using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Abstract
{
    public interface IMaterialDal:IRepository<Material>
    {
        Task<int> GetNumberOfMaterialAsync(int userId);
        Task<ICollection<Material>> GetWithAnnouncementAndAnswerAsync(int givenClasroomId);
    }
}
