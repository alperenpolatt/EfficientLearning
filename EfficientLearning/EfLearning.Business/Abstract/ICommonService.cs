using EfLearning.Business.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface ICommonService
    {
        Task<BasexResponse<TotalScoreResponse>> GetTotalScore(int userId,string role);
        Task<BasexResponse<ICollection<NotificationResponse>>> GetNotifications(int userId);
    }
}
