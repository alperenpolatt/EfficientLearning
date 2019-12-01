using System;
using System.ComponentModel;
namespace EfLearning.Core.Classrooms
{
    [Flags]
    public enum ProgrammingType : byte
    {
        [Description("Python")]
        Python = 0,
        [Description("C Programming Language")]
        C = 1,
        [Description("Java")]
        Java = 2
    }
}
