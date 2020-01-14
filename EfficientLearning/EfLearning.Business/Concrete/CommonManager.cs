using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.EntitiesHelper;
using EfLearning.Data;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class CommonManager:ICommonService
    {
        private IGivenClassroomDal _givenClassroomDal;
        private IMaterialAnswerDal _materialAnswerDal;
        private IDonePracticeDal _donePracticeDal;
        private ITakenClassroomDal _takenClassroomDal;

        public CommonManager(IGivenClassroomDal givenClassroomDal, IMaterialAnswerDal materialAnswerDal, IDonePracticeDal donePracticeDal, ITakenClassroomDal takenClassroomDal)
        {
            _givenClassroomDal = givenClassroomDal;
            _materialAnswerDal = materialAnswerDal;
            _donePracticeDal = donePracticeDal;
            _takenClassroomDal = takenClassroomDal;
        }

        public async Task<BasexResponse<ICollection<NotificationResponse>>> GetNotifications(int userId)
        {
            try
            {
                var practices = await GetDuePracticesAsync(userId);
                var materials = await GetFreshMaterialsAsync(userId);
                var list = new Collection<NotificationResponse>();
                foreach (var item in practices)
                {
                    list.Add(new NotificationResponse
                    {
                        Message = $"Time to do {item.ProgrammingType} {item.Title}",
                        Date = item.Date
                    });
                }
                foreach (var item in materials)
                {
                    list.Add(new NotificationResponse
                    {
                        Message = $"{item.OwnMaterial} shared a material {item.Description}",
                        Date = item.CreationTime
                    });
                }

                return new BasexResponse<ICollection<NotificationResponse>>(list.OrderByDescending(a => a.Date).ToList());

            }
            catch (Exception ex)
            {
                return new BasexResponse<ICollection<NotificationResponse>>(ex.Message);
            }
        }

        public async Task<BasexResponse<TotalScoreResponse>> GetTotalScore(int userId, string role)
        {
            try
            {
                if (role == CustomRoles.Student)
                {
                    var resultMaterialAnswer = await _materialAnswerDal.GetWithMaterialAsync(userId);
                    var filteredResult=resultMaterialAnswer.Where(r => r.Material.MaterialScale > 0);
                    return new BasexResponse<TotalScoreResponse>(new TotalScoreResponse(role)
                    {
                        Total = filteredResult.Sum(ma => ma.Score * (100 / ma.Material.MaterialScale))
                    });
                }
                else if (role == CustomRoles.Teacher) { 
                    var result = await _givenClassroomDal.GetNumberOfStudentsAsync(userId);
                    return new BasexResponse<TotalScoreResponse>(new TotalScoreResponse(role)
                    {
                        Total = result,
                    });
                }
                else
                    return new BasexResponse<TotalScoreResponse>($"There is no point system for {role}");
            }
            catch (Exception ex)
            {
                return new BasexResponse<TotalScoreResponse>(ex.Message);
            }
        }

        private async Task<ICollection<DonePracticeNotificationResponse>> GetDuePracticesAsync(int userId)
        {
                var list = new Collection<DonePracticeNotificationResponse>();
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
                        Title = item.GivenPractice.Title,
                        Date=item.CreationTime.AddSeconds(Math.Pow(2, item.TotalDonePractice))//ne zamandan sonra yapılması gerekiyor
                    });
                }
                return list;
        }

        private async Task<ICollection<CloseMaterialResponse>> GetFreshMaterialsAsync(int userId)
        {
            
                var materials = (await _takenClassroomDal.GetByUserIdWithGivenClassroomAndItsMaterialsAndOwnAsync(userId)).SelectMany(a => a.GivenClassroom.Materials);
                var freshMaterial = materials.Where(m => DateTime.UtcNow.Subtract(m.CreationTime).TotalDays <= 1);


                var list = new Collection<CloseMaterialResponse>();

                foreach (var item in freshMaterial)
                {
                    list.Add(new CloseMaterialResponse
                    {
                        Id = item.Id,
                        Deadline = item.Deadline.Value,
                        Description = item.Announcement.Description,
                        GivenClassroomId = item.GivenClassroomId,
                        CreationTime = item.CreationTime,
                        OwnMaterial=item.GivenClassroom.User.Name +" "+ item.GivenClassroom.User.Surname
                    });
                }
                return list;
            
        }

    }
}
