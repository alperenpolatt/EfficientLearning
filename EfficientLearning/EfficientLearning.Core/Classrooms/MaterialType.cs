using System.ComponentModel;

namespace EfLearning.Core.Classrooms
{
    public enum MaterialType
    {
        [Description("Question")]
        Question = 0,
        [Description("Task")]
        Task = 1,
        [Description("Announcement")]
        Announcement = 2
    }
}
