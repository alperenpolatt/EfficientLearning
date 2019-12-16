using AutoMapper;
using EfLearning.Api.Resources.Classroom;
using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Mapping
{
    public class MaterialAnswerMapping:Profile
    {
        public MaterialAnswerMapping()
        {
            CreateMap<MaterialAnswerResource, MaterialAnswer>();
            CreateMap<MaterialAnswer, MaterialAnswerResource>();

            CreateMap<MaterialAnswerUpdateResource, MaterialAnswer>();
            CreateMap<MaterialAnswer, MaterialAnswerUpdateResource>();
        }
    }
}
