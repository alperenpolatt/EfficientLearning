using EfLearning.Business.Responses;
using EfLearning.Core.Announcements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface IAnnouncementService
    {
        Task<BasexResponse<ICollection<AnnouncementResponse>>> GetAllAsync(int givenClassroomId);
    }
}
