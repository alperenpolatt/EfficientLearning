using EfLearning.Business.Responses;
using EfLearning.Core.Practices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface IDonePracticeService
    {
        Task<BasexResponse<TakenPointResponse>> CreateAsync(DonePractice donePractice);
        Task<BasexResponse<ICollection<DonePracticeNotificationResponse>>> GetByDateAsync(int userId);
    }
}
