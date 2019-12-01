using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business
{
    public static class JwtSettings
    {
        public const string Audience = "http://localhost:5000";
        public const string Issuer = "http://localhost:5000";
        public const int  AccessTokenExpiration =1;
        public const int RefreshTokenExpiration = 60;
        public const string SecurityKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";
    }
}
