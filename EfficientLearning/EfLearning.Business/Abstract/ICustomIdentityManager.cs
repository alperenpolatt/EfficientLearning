using EfLearning.Business.Responses;
using EfLearning.Core.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface ICustomIdentityManager
    {
        Task<UserResponse> RegisterAsync(AppUser user, string password);//only student
        Task<BaseResponse> ConfirmUserAsync(int userId,string token);
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
        Task<AuthenticationResponse> LoginAsync(string email, string password);
        Task<AuthenticationResponse> RefreshTokenAsync(string token, string refreshToken);

    }
}
