using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Security
{
    public class TokenOptions
    {
        public string Audience { get; set; }//Tokenı kimler kullanabilecek (www.xyz.com)
        public string Issuer { get; set; }//Tokenı dağatan kim
        public int AccessTokenExpiration { get; set; }//Tokenın ömrü
        public int RefreshTokenExpiration { get; set; }//Token dolduğunda gönderilecek olan reftokun ömrü buda dolarsa yeniden token alması gerekecek
        public string SecurityKey { get; set; }//Secret key imzası
    }
}
