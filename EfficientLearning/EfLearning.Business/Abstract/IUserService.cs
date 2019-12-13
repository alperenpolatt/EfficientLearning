using EfLearning.Business.Responses;
using EfLearning.Core.Users;
using EfLearning.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface IUserService
    {
        Task<BasexResponse<ICollection<UserCountByMonthResponse>>> GetRegisteredUsersByMonthAsync(int month,string roleName);
        Task<BasexResponse<ICollection<AppUser>>> GetUsersByRoleAsync(string roleName);
        Task<BasexResponse<SimpleUserResponse>> GetUserByEmailWithRoleAsync(string email);
        Task<BasexResponse<AppUser>> CreateStudentAsync(AppUser user,string password);
        Task<BasexResponse<AppUser>> CreateTeacherAsync(AppUser user,string password);
        Task<BasexResponse<AppUser>> UpdateUserAsync(AppUser user);
        Task<BasexResponse<AppUser>> DeleteUserByIdAsync(int userId);
    }
}
