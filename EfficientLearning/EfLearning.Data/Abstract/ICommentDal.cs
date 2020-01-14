using EfLearning.Core.Announcements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Abstract
{
    public interface ICommentDal:IRepository<Comment>
    {
        Task<ICollection<Comment>> GetWithUserAsync(int announcementId);
    }
}
