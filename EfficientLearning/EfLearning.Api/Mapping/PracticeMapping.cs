using AutoMapper;
using EfLearning.Api.Resources.Practice;
using EfLearning.Core.Practices;

namespace EfLearning.Api.Mapping
{
    public class PracticeMapping:Profile
    {
        public PracticeMapping()
        {
            CreateMap<DonePracticeResource, DonePractice>();
            CreateMap<DonePractice, DonePracticeResource>();
        }
    }
}
