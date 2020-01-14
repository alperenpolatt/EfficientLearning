using EfLearning.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Responses
{
    public class TotalScoreResponse
    {
        public TotalScoreResponse(string role)
        {
            _role = role;
        }
        private readonly string _role;
        public int? Total { get; set; }
        public int? Star
        {
            get {
                if (_role == CustomRoles.Student)
                    return Total / 100;
                else if (_role == CustomRoles.Teacher)
                    return Total / 5;
                else
                    return default(int);
            }
        }
    }
}
