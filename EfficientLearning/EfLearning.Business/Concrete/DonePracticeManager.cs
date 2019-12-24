using EfLearning.Business.Abstract;
using EfLearning.Business.Common;
using EfLearning.Business.Responses;
using EfLearning.Core.EntitiesHelper;
using EfLearning.Core.Practices;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class DonePracticeManager : IDonePracticeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IDonePracticeDal _donePracticeDal;
        public DonePracticeManager(IUnitOfWork unitOfWork, IDonePracticeDal donePracticeDal)
        {
            _unitOfWork = unitOfWork;
            _donePracticeDal = donePracticeDal;
        }

        public async Task<BasexResponse<TakenPointResponse>> CreateAsync(DonePractice donePractice)
        {
            try
            {
                var resDonePractice = await _donePracticeDal.FindByAsync(d => d.UserId == donePractice.UserId && d.GivenPracticeId == donePractice.GivenPracticeId);
                var singleResDone = resDonePractice.FirstOrDefault();
                if (singleResDone!= null)
                {
                    singleResDone.TotalDonePractice++;
                    singleResDone.CreationTime = DateTime.UtcNow;
                    await _donePracticeDal.UpdateAsync(singleResDone);
                    await _unitOfWork.CompleteAsync();
                    var updatedResponse = new TakenPointResponse
                    {
                        Score = singleResDone.TotalDonePractice * 100,
                        UserId = (int)singleResDone.UserId,
                        GivenPracticeId=singleResDone.GivenPracticeId
                    };
                    return new BasexResponse<TakenPointResponse>(updatedResponse);
                }
                donePractice.CreationTime = DateTime.UtcNow;
                donePractice.TotalDonePractice = 1;
                await _donePracticeDal.AddAsync(donePractice);
                await _unitOfWork.CompleteAsync();
                var response = new TakenPointResponse
                {
                    Score = donePractice.TotalDonePractice * 100,
                    UserId = (int)donePractice.UserId,
                    GivenPracticeId=donePractice.GivenPracticeId
                };
                return new BasexResponse<TakenPointResponse>(response);
            }
            catch (Exception ex)
            {
                return new BasexResponse<TakenPointResponse>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<DonePracticeNotificationResponse>>> GetByDateAsync(int userId)
        {
            try
            {
                var list=new Collection<DonePracticeNotificationResponse>();
                var response = await _donePracticeDal.GetByUserIdWithGivenPracticeAsync(userId);
                var filterResponse = response.Where(dp =>
                  (dp.CreationTime.AddSeconds(Math.Pow(2, dp.TotalDonePractice))
                  <= DateTime.UtcNow));
                foreach (var item in filterResponse)
                {
                    list.Add(new DonePracticeNotificationResponse
                    {
                        GivenPracticeId = item.GivenPracticeId,
                        Level = item.GivenPractice.Level,
                        ProgrammingType = item.GivenPractice.ProgrammingType.Description(),
                        Title = item.GivenPractice.Title
                    });
                }

                return new BasexResponse<ICollection<DonePracticeNotificationResponse>>(list);
                
            }
            catch (Exception ex)
            {
                return new BasexResponse<ICollection<DonePracticeNotificationResponse>>(ex.Message);
            }
        }
    }
}
