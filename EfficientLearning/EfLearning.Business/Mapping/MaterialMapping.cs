using AutoMapper;
using EfLearning.Business.Responses;
using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Business.Mapping
{
    class MaterialMapping:Profile
    {
        public MaterialMapping()
        {
            CreateMap<Material, MaterialQuestionResponse>();
        }
    }
}
