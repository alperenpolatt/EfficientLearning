using EfLearning.Core.Announcements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfLearning.Data.Abstract
{
    public interface IAnnouncementDal : IRepository<Announcement>
    {
        Task<ICollection<Announcement>> GetAllWithMaterialAndComments(int givenClassroomId);
    }
}
