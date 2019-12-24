using AutoMapper;
using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Users;
using EfLearning.Data;
using EfLearning.Data.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<AppUser> _aspUserManager;
        private RoleManager<AppRole> _aspRoleManager;
        private IUserDal _userDal;
        private readonly IMapper _mapper;
        public UserManager(IUnitOfWork unitOfWork, UserManager<AppUser> aspUserManager, RoleManager<AppRole> aspRoleManager,IUserDal userDal,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _aspUserManager = aspUserManager;
            _aspRoleManager = aspRoleManager;
            _userDal = userDal;
            _mapper = mapper;
        }

        public async Task<BasexResponse<AppUser>> CreateStudentAsync(AppUser user,string password)
        {
            user.CreationTime = DateTime.UtcNow;
            var resultCreate = await _aspUserManager.CreateAsync(user, password);
            if (!resultCreate.Succeeded)
                return new BasexResponse<AppUser>(resultCreate.Errors.FirstOrDefault().Description);


            var resultRole = await _aspUserManager.AddToRoleAsync(user, CustomRoles.Student);
            if (!resultRole.Succeeded)
                return new BasexResponse<AppUser>(resultRole.Errors.FirstOrDefault().Description);

            await _unitOfWork.CompleteAsync();
            return new BasexResponse<AppUser>(user);
        }

        public async Task<BasexResponse<AppUser>> CreateTeacherAsync(AppUser user, string password)
        {
            user.CreationTime = DateTime.UtcNow;
            var resultCreate = await _aspUserManager.CreateAsync(user, password);
            if (!resultCreate.Succeeded)
                return new BasexResponse<AppUser>(resultCreate.Errors.FirstOrDefault().Description);


            var resultRole = await _aspUserManager.AddToRoleAsync(user, CustomRoles.Teacher);
            if (!resultRole.Succeeded)
                return new BasexResponse<AppUser>(resultRole.Errors.FirstOrDefault().Description);

            await _unitOfWork.CompleteAsync();
            return new BasexResponse<AppUser>(user);
        }

        public async Task<BasexResponse<AppUser>> DeleteUserByIdAsync(int userId)
        {
            if (!_aspUserManager.Users.Any(u=>u.Id==userId))
            {
                return new BasexResponse<AppUser>("There is no this id");
            }
            var resultUser = await _aspUserManager.FindByIdAsync(userId.ToString());
            
            var resultDelete = await _aspUserManager.DeleteAsync(resultUser);
            if (resultDelete.Succeeded)
            {
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<AppUser>(resultUser);
            }
            return new BasexResponse<AppUser>(resultDelete.Errors.FirstOrDefault().Description);

        }

        public async Task<BasexResponse<ICollection<UserCountByMonthResponse>>> GetRegisteredUsersByMonthAsync(int month,string roleName)
        {
            var response=new Collection<UserCountByMonthResponse>();
            var lastXMonth = DateTime.UtcNow.AddMonths(-month);
            var users = await _aspUserManager.GetUsersInRoleAsync(roleName);
            var lastXMonthUsers = users.Where(u => u.CreationTime > lastXMonth);
            var groupedUsers = lastXMonthUsers.GroupBy(u => u.CreationTime.Month);
            foreach (var item in groupedUsers)
            {
                response.Add(new UserCountByMonthResponse
                {
                    UserCount=item.Count(),
                    MonthName= CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(item.Key)
                });
            }

          

            return new BasexResponse<ICollection<UserCountByMonthResponse>>(response);
        }

        public async Task<BasexResponse<SimpleUserResponse>> GetUserByEmailWithRoleAsync(string email)
        {
            var user = await _aspUserManager.FindByEmailAsync(email);
            if (user == null)
                return new BasexResponse<SimpleUserResponse>("There is no user with this email ");
            var role = await _aspUserManager.GetRolesAsync(user);

            var simpleUser = _mapper.Map<AppUser, SimpleUserResponse>(user);
            simpleUser.RoleName = role.FirstOrDefault();
            return new BasexResponse<SimpleUserResponse>(simpleUser);
            
        }

        public async Task<BasexResponse<ICollection<AppUser>>> GetUsersByRoleAsync(string roleName)
        {
            if (String.IsNullOrEmpty(roleName))
                return new BasexResponse<ICollection<AppUser>>("Role can not be null");
            if (!_aspRoleManager.Roles.Any(r=>r.Name== roleName))
                return new BasexResponse<ICollection<AppUser>>("There is no this role");

            var users=await _aspUserManager.GetUsersInRoleAsync(roleName);
            if (users.Count==0)
            {
                return new BasexResponse<ICollection<AppUser>>("There is no user in this role");
            }
            return new BasexResponse<ICollection<AppUser>>(users);
            
        }

        public async Task<BasexResponse<AppUser>> UpdateUserAsync(AppUser user)
        {
            var resultUser = await _aspUserManager.FindByIdAsync(user.Id.ToString());
            resultUser.Name = user.Name;
            resultUser.Surname = user.Surname;

            var resultUpdate = await _aspUserManager.UpdateAsync(resultUser);
            if (resultUpdate.Succeeded)
            {
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<AppUser>(resultUser);
            }
            return new BasexResponse<AppUser>(resultUpdate.Errors.FirstOrDefault().Description);
        }


        private async Task CheckThereIsRoles()
        {
            if (await _aspRoleManager.Roles.CountAsync() == 0)
            {
                await _aspRoleManager.CreateAsync(new AppRole { Name = CustomRoles.Admin });
                await _aspRoleManager.CreateAsync(new AppRole { Name = CustomRoles.Teacher });
                await _aspRoleManager.CreateAsync(new AppRole { Name = CustomRoles.Student });
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<BasexResponse<SimpleUserResponse>> GetUserByIdWithRoleAsync(int userId)
        {
            var user = await _aspUserManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return new BasexResponse<SimpleUserResponse>("There is no user with this id ");
            var role = await _aspUserManager.GetRolesAsync(user);

            var simpleUser = _mapper.Map<AppUser, SimpleUserResponse>(user);
            simpleUser.RoleName = role.FirstOrDefault();
            return new BasexResponse<SimpleUserResponse>(simpleUser);
        }
    }
}
