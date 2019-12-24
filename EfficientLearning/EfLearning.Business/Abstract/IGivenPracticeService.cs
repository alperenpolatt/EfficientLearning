using EfLearning.Business.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface IGivenPracticeService
    {
        Task<BasexResponse<ICollection<LevelResponse>>> GetLevelsAsync(int programmingType);
        Task<BasexResponse<LevelDetailResponse>> GetLevelDetailAsync(int id);
        Task<BasexResponse<AnswerResponse>> CheckAnswerAsync(int id,string answer);
        Task<BasexResponse<HintResponse>> GetAHintAsync(int id);
    }
}
