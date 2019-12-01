﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public BaseResponse(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
    }
}
