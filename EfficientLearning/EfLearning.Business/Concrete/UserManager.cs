using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Users;
using EfLearning.Data;
using EfLearning.Data.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<AppUser> _aspUserManager;
        private RoleManager<AppRole> _aspRoleManager;
        private IUserDal _userDal;
        public UserManager(IUnitOfWork unitOfWork, UserManager<AppUser> aspUserManager, RoleManager<AppRole> aspRoleManager,IUserDal userDal)
        {
            _unitOfWork = unitOfWork;
            _aspUserManager = aspUserManager;
            _aspRoleManager = aspRoleManager;
            _userDal = userDal;
        }

        public async Task<UserResponse> CreateStudentAsync(AppUser user,string password)
        {
            user.CreationTime = DateTime.UtcNow;
            var resultCreate = await _aspUserManager.CreateAsync(user, password);
            if (!resultCreate.Succeeded)
                return new UserResponse(resultCreate.Errors.FirstOrDefault().Description);

            if (_aspRoleManager.Roles.Count() == 0)
            {
                await _aspRoleManager.CreateAsync(new AppRole { Name = CustomRoles.Admin });
                await _aspRoleManager.CreateAsync(new AppRole { Name = CustomRoles.Teacher });
                await _aspRoleManager.CreateAsync(new AppRole { Name = CustomRoles.Student });
                await _unitOfWork.CompleteAsync();
            }

            var resultRole = await _aspUserManager.AddToRoleAsync(user, CustomRoles.Student);
            if (!resultRole.Succeeded)
                return new UserResponse(resultRole.Errors.FirstOrDefault().Description);

            await _unitOfWork.CompleteAsync();
            return new UserResponse(user);
        }

        public async Task<UserResponse> CreateTeacherAsync(AppUser user, string password)
        {
            user.CreationTime = DateTime.UtcNow;
            var resultCreate = await _aspUserManager.CreateAsync(user, password);
            if (!resultCreate.Succeeded)
                return new UserResponse(resultCreate.Errors.FirstOrDefault().Description);

            if (_aspRoleManager.Roles.Count() == 0)
            {
                await _aspRoleManager.CreateAsync(new AppRole { Name = CustomRoles.Admin });
                await _aspRoleManager.CreateAsync(new AppRole { Name = CustomRoles.Teacher });
                await _aspRoleManager.CreateAsync(new AppRole { Name = CustomRoles.Student });
                await _unitOfWork.CompleteAsync();
            }

            var resultRole = await _aspUserManager.AddToRoleAsync(user, CustomRoles.Teacher);
            if (!resultRole.Succeeded)
                return new UserResponse(resultRole.Errors.FirstOrDefault().Description);

            await _unitOfWork.CompleteAsync();
            return new UserResponse(user);
        }

        public async Task<UserResponse> DeleteUserByIdAsync(int userId)
        {
            if (!_aspUserManager.Users.Any(u=>u.Id==userId))
            {
                return new UserResponse("There is no this id");
            }
            var resultUser = await _aspUserManager.FindByIdAsync(userId.ToString());
            
            var resultDelete = await _aspUserManager.DeleteAsync(resultUser);
            if (resultDelete.Succeeded)
            {
                await _unitOfWork.CompleteAsync();
                return new UserResponse(resultUser);
            }
            return new UserResponse(resultDelete.Errors.FirstOrDefault().Description);

        }

        public async Task<UserListResponse> GetUsersByRoleAsync(string roleName)
        {
            if (String.IsNullOrEmpty(roleName))
                return new UserListResponse("Role can not be null");
            if (!_aspRoleManager.Roles.Any(r=>r.Name== roleName))
                return new UserListResponse("There is no this role");

            var users=await _aspUserManager.GetUsersInRoleAsync(roleName);
            if (users.Count==0)
            {
                return new UserListResponse("There is no user in this role");
            }
            return new UserListResponse(users);
            
        }

        public async Task<UserResponse> UpdateUserAsync(AppUser user)
        {
            var resultUser = await _aspUserManager.FindByIdAsync(user.Id.ToString());
            resultUser.Name = user.Name;
            resultUser.Surname = user.Surname;

            var resultUpdate = await _aspUserManager.UpdateAsync(resultUser);
            if (resultUpdate.Succeeded)
            {
                await _unitOfWork.CompleteAsync();
                return new UserResponse(user);
            }
            return new UserResponse(resultUpdate.Errors.FirstOrDefault().Description);


        }
    }
}
