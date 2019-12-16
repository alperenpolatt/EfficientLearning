using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Classrooms;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class MaterailAnswerManager:IMaterialAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMaterialAnswerDal _materialAnswerDal;
        public MaterailAnswerManager(IUnitOfWork unitOfWork, IMaterialAnswerDal materialAnswerDal)
        {
            _unitOfWork = unitOfWork;
            _materialAnswerDal = materialAnswerDal;
        }

        public async Task<BasexResponse<MaterialAnswer>> CreateAsync(MaterialAnswer materialAnswer)
        {
            try
            {
                materialAnswer.CreationTime= DateTime.UtcNow;
                await _materialAnswerDal.AddAsync(materialAnswer);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<MaterialAnswer>(materialAnswer);
            }
            catch (Exception ex)
            {
                return new BasexResponse<MaterialAnswer>(ex.Message);
            }
        }

        public async Task<BasexResponse<MaterialAnswer>> DeleteByIdAsync(int userId,int materialId)
        {
            try
            {
                var materialAnswer=await _materialAnswerDal.DeleteByCompositeKeysAsync(userId,materialId);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<MaterialAnswer>(materialAnswer);
            }
            catch (Exception ex)
            {

                return new BasexResponse<MaterialAnswer>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<MaterialAnswer>>> GetByMaterialId(int materialId)
        {
            try
            {
                var materialAnswers = await _materialAnswerDal.GetByMaterialIdWithUsersAsync(materialId);
                return new BasexResponse<ICollection<MaterialAnswer>>(materialAnswers);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<MaterialAnswer>>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<ScoreResponse>>> GetSumOfPointsByGivenClassroomId(int givenClassroomId, int month)
        {
            try
            {
                var response = new Collection<ScoreResponse>();
                var materialAnswers =await _materialAnswerDal.GetWithMaterialAndUserAsync(givenClassroomId);
                var lastXMonth = DateTime.UtcNow.AddMonths(-month);
                var lastXMonthUsers = materialAnswers.Where(u => u.CreationTime > lastXMonth);
                var groupedUsers = lastXMonthUsers.GroupBy(u => u.UserId);
                foreach (var item in groupedUsers)
                {
                    response.Add(new ScoreResponse
                    {
                        UserId=item.FirstOrDefault().UserId,
                        Name=item.FirstOrDefault().User.Name,
                        Surname=item.FirstOrDefault().User.Surname,
                        Username=item.FirstOrDefault().User.UserName,
                        TotalScore=item.Sum(s=>s.Score)
                    });
                }
                var orderedResponse=response.OrderByDescending(a => a.TotalScore).ToList();
                return new BasexResponse<ICollection<ScoreResponse>>(orderedResponse);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<ScoreResponse>>(ex.Message);
            }
        }

        public async Task<BasexResponse<MaterialAnswer>> UpdateAsync(MaterialAnswer materialAnswer)
        {
            try
            {
                var resultMaterialAnswer = await _materialAnswerDal.FindAsync(ma => ma.MaterialId==materialAnswer.MaterialId && ma.UserId==materialAnswer.UserId);
                if (resultMaterialAnswer == null)
                    return new BasexResponse<MaterialAnswer>("Student has not done yet");
                resultMaterialAnswer.Score = materialAnswer.Score;

                var updatedMaterialAnswer = await _materialAnswerDal.UpdateAsync(resultMaterialAnswer);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<MaterialAnswer>(resultMaterialAnswer);
            }
            catch (Exception ex)
            {
                return new BasexResponse<MaterialAnswer>(ex.Message);
            }
        }
    }
}
