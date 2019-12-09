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
        Task<UserListResponse> GetUsersByRoleAsync(string roleName);
        Task<UserResponse> CreateStudentAsync(AppUser user,string password);
        Task<UserResponse> CreateTeacherAsync(AppUser user,string password);
        Task<UserResponse> UpdateUserAsync(AppUser user);
        Task<UserResponse> DeleteUserByIdAsync(int userId);
    }
}
