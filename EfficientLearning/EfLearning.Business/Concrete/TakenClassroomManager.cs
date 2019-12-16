using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Classrooms;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class TakenClassroomManager: ITakenClassroomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ITakenClassroomDal _takenClassroomDal;
        public TakenClassroomManager(IUnitOfWork unitOfWork, ITakenClassroomDal takenClassroomDal)
        {
            _unitOfWork = unitOfWork;
            _takenClassroomDal = takenClassroomDal;
        }

        public async Task<BasexResponse<TakenClassroom>> CreateAsync(TakenClassroom takenClassroom)
        {
            try
            {
                takenClassroom.CreationTime = DateTime.UtcNow;
                await _takenClassroomDal.AddAsync(takenClassroom);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<TakenClassroom>(takenClassroom);
            }
            catch (Exception ex)
            {
                return new BasexResponse<TakenClassroom>(ex.InnerException?.Message);
            }
        }

        public async Task<BasexResponse<TakenClassroom>> DeleteAsync(int userId, int givenClassroomId)
        {
            try
            {
                var takenClassroom=await _takenClassroomDal.DeleteByCompositeKeysAsync(userId,givenClassroomId);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<TakenClassroom>(takenClassroom);
            }
            catch (Exception ex)
            {

                return new BasexResponse<TakenClassroom>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<TakenClassroom>>> GetByUserIdAsync(int userId)
        {
            try
            {
                var takenClassrooms = await _takenClassroomDal.GetByUserIdWithGivenClassroomAndCourseAsync(userId);
                return new BasexResponse<ICollection<TakenClassroom>>(takenClassrooms);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<TakenClassroom>>(ex.Message);
            }
        }
    }
}
