using AutoMapper;
using EfLearning.Api.Resources.Course;
using EfLearning.Core.Classrooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfLearning.Api.Mapping
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {
            CreateMap<CourseResource, Course>();
            CreateMap<Course, CourseResource>();

            CreateMap<CourseUpdateResource, Course>();
            CreateMap<Course, CourseUpdateResource>();
        }
    }
}
