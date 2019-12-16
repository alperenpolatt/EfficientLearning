using EfLearning.Business.Responses;
using EfLearning.Core.Announcements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfLearning.Business.Abstract
{
    public interface ICommentService
    {
        Task<BasexResponse<ICollection<CommentResponse>>> GetAllAsync(int announcementId);
        Task<BasexResponse<Comment>> CreateAsync(Comment comment);
        Task<BasexResponse<Comment>> DeleteByIdAsync(int commentId);
    }
}
