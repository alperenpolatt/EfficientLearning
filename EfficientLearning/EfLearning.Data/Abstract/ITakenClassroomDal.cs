using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Abstract
{
    public interface ITakenClassroomDal : IRepository<TakenClassroom>
    {
        Task<ICollection<TakenClassroom>> GetByUserIdWithGivenClassroomAndCourseAsync(int userId);
        Task<ICollection<TakenClassroom>> GetByUserIdWithGivenClassroomAndItsMaterialsAsync(int userId);
        Task<ICollection<TakenClassroom>> GetByUserIdWithGivenClassroomAndItsMaterialsAndOwnAsync(int userId);
        Task<TakenClassroom> DeleteByCompositeKeysAsync(int userId, int givenClassroomId);
    }
}
