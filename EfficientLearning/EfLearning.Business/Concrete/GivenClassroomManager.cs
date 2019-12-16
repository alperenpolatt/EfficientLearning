using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Classrooms;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class GivenClassroomManager : IGivenClassroomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IGivenClassroomDal _givenClassroomDal;
        public GivenClassroomManager(IUnitOfWork unitOfWork,IGivenClassroomDal givenClassroomDal)
        {
            _unitOfWork = unitOfWork;
            _givenClassroomDal = givenClassroomDal;
        }

        public async Task<BasexResponse<GivenClassroom>> CreateAsync(GivenClassroom givenClassroom)
        {
            try
            {
                givenClassroom.CreationTime = DateTime.UtcNow;
                await _givenClassroomDal.AddAsync(givenClassroom);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<GivenClassroom>(givenClassroom);
            }
            catch (Exception ex)
            {
                return new BasexResponse<GivenClassroom>(ex.Message);
            }
        }

        public async Task<BasexResponse<GivenClassroom>> DeleteByIdAsync(int givenClassroomId)
        {
            try
            {
                var givenClassroom = await _givenClassroomDal.GetAsync(givenClassroomId);
                 _givenClassroomDal.Delete(givenClassroom);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<GivenClassroom>(givenClassroom);
            }
            catch (Exception ex)
            {

                return new BasexResponse<GivenClassroom>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<GivenClassroom>>> GetAllAsync()
        {
            try
            {
                var givenClassrooms = await _givenClassroomDal.GetAllAsync();
                return new BasexResponse<ICollection<GivenClassroom>>(givenClassrooms);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<GivenClassroom>>(ex.Message);
            }
        }

        public async Task<BasexResponse<GivenClassroom>> GetByIdAsync(int id)
        {
            try
            {
                var givenClassroom = await _givenClassroomDal.GetByIdWithTakenClasroomsAndStudentsAsync(id);
                return new BasexResponse<GivenClassroom>(givenClassroom);
            }
            catch (Exception ex)
            {

                return new BasexResponse<GivenClassroom>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<GivenClassroom>>> GetBySearchTermAsync(string query)
        {
            try
            {
                var givenClassrooms = await _givenClassroomDal.GetBySearchTermAsync(query);
                return new BasexResponse<ICollection<GivenClassroom>>(givenClassrooms);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<GivenClassroom>>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<GivenClassroom>>> GetByUserIdAsync(int userId)
        {
            try
            {
                var givenClassrooms=await _givenClassroomDal.FindByAsync(u => u.UserId == userId);
                return new BasexResponse<ICollection<GivenClassroom>>(givenClassrooms);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<GivenClassroom>>(ex.Message);
            }
        }

        public async Task<BasexResponse<GivenClassroom>> UpdateAsync(GivenClassroom givenClassroom)
        {
            try
            {
                var resultGivenClassroom = await _givenClassroomDal.GetAsync(givenClassroom.Id);
                resultGivenClassroom.CourseId = givenClassroom.CourseId;
                resultGivenClassroom.Description = givenClassroom.Description;

                var updatedClassroom = await _givenClassroomDal.UpdateAsync(resultGivenClassroom, resultGivenClassroom.Id);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<GivenClassroom>(updatedClassroom);
            }
            catch (Exception ex)
            {
                return new BasexResponse<GivenClassroom>(ex.Message);
            }
        }
    }
}
