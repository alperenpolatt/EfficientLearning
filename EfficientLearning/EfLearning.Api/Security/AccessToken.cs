using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Security
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }//Tokenın ömrü
        public string RefreshToken { get; set; }
    }
}
