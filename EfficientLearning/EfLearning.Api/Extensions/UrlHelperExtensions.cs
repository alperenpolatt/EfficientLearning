using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace EfLearning.Api.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, int userId, string code, string scheme)
        {
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            return urlHelper.Action(
                action: "ConfirmEmail",
                controller: "Account",
                values: new { userId = userId ,token = code },
                protocol: scheme);
        }

       
    }
}
