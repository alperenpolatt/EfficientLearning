using AutoMapper;
using EfLearning.Api.Resources.Classroom;
using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Mapping
{
    public class GivenClassroomMapping:Profile
    {
        public GivenClassroomMapping()
        {
            CreateMap<GivenClassroomResource, GivenClassroom>();
            CreateMap<GivenClassroom, GivenClassroomResource>();

            CreateMap<GivenClassroomUpdateResource, GivenClassroom>();
            CreateMap<GivenClassroom, GivenClassroomUpdateResource>();
        }
    }
}
