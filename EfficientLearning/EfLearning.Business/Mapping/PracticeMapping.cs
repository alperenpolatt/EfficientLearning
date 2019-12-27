using AutoMapper;
using EfLearning.Business.Responses;
using EfLearning.Core.Practices;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Mapping
{
    class PracticeMapping:Profile
    {
        public PracticeMapping()
        {
            CreateMap<GivenPractice, LevelResponse>();
            CreateMap<GivenPractice, LevelDetailResponse>();
            CreateMap<DonePractice, DonePracticeNotificationResponse>();
        }
    }
}
