using EfLearning.Business.Responses;
using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface ICourseService
    {
        Task<BasexResponse<ICollection<Course>>> GetAllAsync();
        Task<BasexResponse<Course>> CreateAsync(Course course);
        Task<BasexResponse<Course>> UpdateAsync(Course course);
        Task<BasexResponse<Course>> DeleteByIdAsync(int courseId);
    }
}
