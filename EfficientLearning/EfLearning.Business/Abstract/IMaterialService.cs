using EfLearning.Business.Responses;
using EfLearning.Core.Classrooms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface IMaterialService
    {
        Task<BasexResponse<ICollection<Material>>> GetMaterialsByGivenClassroomIdAndUserId(int givenClassroomId, int userId);
        Task<BasexResponse<ICollection<CloseMaterialResponse>>> GetCloseMaterials(int userId);
        Task<BasexResponse<ICollection<CloseMaterialResponse>>> GetFreshMaterials(int userId);
        Task<BasexResponse<CountResponse>> GetMaterialCountAsync(int userId);
        Task<BasexResponse<MaterialQuestionResponse>> GetByIdAsync(int id);
        Task<BasexResponse<ICollection<Material>>> GetMaterialsByGivenClassroomId(int givenClassroomId);
        Task<BasexResponse<ICollection<MaterialResultResponse>>> GetMaterialsAndAnwers(int userId,int givenClassroomId);
        Task<BasexResponse<Material>> CreateAsync(Material material,string description);
        Task<BasexResponse<Material>> UpdateAsync(Material material, string description);
        Task<BasexResponse<Material>> DeleteByIdAsync(int materialId);
    }
}
