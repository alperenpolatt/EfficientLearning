﻿namespace EfLearning.Business
{
    public static class JwtSettings
    {
        public const string Audience = "http://localhost:57859";
        public const string Issuer = "http://localhost:57859";
        public const int  AccessTokenExpiration =1;
        public const int RefreshTokenExpiration = 2;
        public const string SecurityKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";
    }
}
