using EfLearning.Business.Responses;
using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface IMaterialAnswerService
    {
        Task<BasexResponse<ICollection<ScoreResponse>>> GetSumOfPointsByGivenClassroomId(int givenClassroomId,int month);
        Task<BasexResponse<ICollection<MaterialAnswer>>> GetByMaterialId(int materialId);
        Task<BasexResponse<MaterialAnswer>> CreateAsync(MaterialAnswer materialAnswer);
        Task<BasexResponse<MaterialAnswer>> UpdateAsync(MaterialAnswer material);
        Task<BasexResponse<MaterialAnswer>> DeleteByIdAsync(int userId, int materialId);
    }
}
