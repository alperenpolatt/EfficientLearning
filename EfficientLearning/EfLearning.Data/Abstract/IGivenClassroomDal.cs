using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Abstract
{
    public interface IGivenClassroomDal : IRepository<GivenClassroom>
    {
        Task<GivenClassroom> GetByIdWithTakenClasroomsAndStudentsAsync(int id);
        Task<ICollection<GivenClassroom>> GetBySearchTermAsync(string query);
    }
}
