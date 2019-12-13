using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Classrooms;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace EfLearning.Business.Concrete
{
    public class CourseManager:ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ICourseDal _courseDal;
        public CourseManager(IUnitOfWork unitOfWork, ICourseDal courseDal)
        {
            _unitOfWork = unitOfWork;
            _courseDal = courseDal;
        }

        public async Task<BasexResponse<Course>> CreateAsync(Course course)
        {
            try
            {
                await _courseDal.AddAsync(course);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<Course>(course);
            }
            catch (Exception ex)
            {
                return new BasexResponse<Course>(ex.Message);
            }
        }

        public async Task<BasexResponse<Course>> DeleteByIdAsync(int courseId)
        {
            try
            {
                var course = await _courseDal.GetAsync(courseId);
                _courseDal.Delete(course);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<Course>(course);
            }
            catch (Exception ex)
            {

                return new BasexResponse<Course>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<Course>>> GetAllAsync()
        {
            try
            {
                var courses =await _courseDal.GetAllAsync();
                return new BasexResponse<ICollection<Course>>(courses);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<Course>>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<CoursePopularityResponse>>> GetPopularityofProgrammingTypesAsync()
        {
            try
            {
                var response = new Collection<CoursePopularityResponse>();
                var groupCourses =await _courseDal.GetCoursesGroupByProgrammingType();
                foreach (var item in groupCourses)
                {
                    response.Add(new CoursePopularityResponse
                    {
                        ProgrammingType= item.Key,
                        Count=item.Count()
                    });
                }
                return new BasexResponse<ICollection<CoursePopularityResponse>>(response);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<CoursePopularityResponse>>(ex.Message);
            }
        }

        public async Task<BasexResponse<Course>> UpdateAsync(Course course)
        {
            try
            {
                var updatedClassroom = await _courseDal.UpdateAsync(course, course.Id);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<Course>(updatedClassroom);
            }
            catch (Exception ex)
            {

                return new BasexResponse<Course>(ex.Message);
            }
        }
    }
}
