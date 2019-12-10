using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class BasexResponse<T> where T : class

    {
        public T Extra { get; set; }


        public bool Success { get; set; }
        public string Message { get; set; }

        //Success
        public BasexResponse(T extra = null)
        {
            this.Extra = extra;
            this.Success = true;
        }
        //Fail
        public BasexResponse(string message)
        {
            this.Success = false;
            this.Message = message;
        }
    }
}
