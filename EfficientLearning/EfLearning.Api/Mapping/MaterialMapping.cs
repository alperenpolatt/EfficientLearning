using AutoMapper;
using EfLearning.Api.Resources.Classroom;
using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Mapping
{
    public class MaterialMapping:Profile
    {
        public MaterialMapping()
        {
            CreateMap<MaterialResource, Material>();
            CreateMap<Material, MaterialResource>();

            CreateMap<MaterialUpdateResource, Material>();
            CreateMap<Material, MaterialUpdateResource>();
        }
    }
}
