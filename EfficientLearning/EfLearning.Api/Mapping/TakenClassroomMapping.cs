using AutoMapper;
using EfLearning.Api.Resources.Classroom;
using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Mapping
{
    public class TakenClassroomMapping:Profile
    {
        public TakenClassroomMapping()
        {
            CreateMap<TakenClassroomResource, TakenClassroom>();
            CreateMap<TakenClassroom, TakenClassroomResource>();
        }
    }
}
