using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class AuthenticationResponse:BaseResponse
    {

        public string Token { get; set; }

        public string RefreshToken { get; set; }
        //success

        public AuthenticationResponse() : base(true, String.Empty)
        {
        }

        //fail

        public AuthenticationResponse(string message) : base(false, message)
        {
        }
    }
}
