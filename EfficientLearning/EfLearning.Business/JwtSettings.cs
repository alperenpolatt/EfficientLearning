namespace EfLearning.Business
{
    public static class JwtSettings
    {
        public const string Audience = "http://localhost:57859";
        public const string Issuer = "http://localhost:57859";
        public const int  AccessTokenExpiration =360;
        public const int RefreshTokenExpiration = 720;
        public const string SecurityKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";
    }
}
