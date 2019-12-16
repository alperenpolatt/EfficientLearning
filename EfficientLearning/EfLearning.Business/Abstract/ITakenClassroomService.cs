using EfLearning.Business.Responses;
using EfLearning.Core.Classrooms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface ITakenClassroomService
    {
        Task<BasexResponse<ICollection<TakenClassroom>>> GetByUserIdAsync(int userId);
        Task<BasexResponse<TakenClassroom>> CreateAsync(TakenClassroom takenClassroom);
        Task<BasexResponse<TakenClassroom>> DeleteAsync(int userId, int givenClassroomId);
    }
}
