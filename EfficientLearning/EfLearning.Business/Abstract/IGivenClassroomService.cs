using EfLearning.Business.Responses;
using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface IGivenClassroomService
    {
        Task<BasexResponse<ICollection<GivenClassroom>>> GetAllAsync();
        Task<BasexResponse<ICollection<GivenClassroom>>> GetByUserIdAsync(int userId);
        Task<BasexResponse<ICollection<GivenClassroom>>> GetBySearchTermAsync(string query);
        Task<BasexResponse<GivenClassroom>> GetByIdAsync(int id);
        Task<BasexResponse<GivenClassroom>> CreateAsync(GivenClassroom givenClassroom);
        Task<BasexResponse<GivenClassroom>> UpdateAsync(GivenClassroom givenClassroom);
        Task<BasexResponse<GivenClassroom>> DeleteByIdAsync(int givenClassroomId);
        Task<BasexResponse<CountResponse>> GetStudentsCountAsync(int userId);
        Task<BasexResponse<ICollection<PopularClassResponse>>> GetByMostStudentsAsync();
        Task<BasexResponse<TotalScoreResponse>> GetTotalScore(int userId);
    }
}
