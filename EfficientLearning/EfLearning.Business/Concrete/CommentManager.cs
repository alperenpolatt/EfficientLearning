using AutoMapper;
using EfLearning.Business.Abstract;
using EfLearning.Business.Responses;
using EfLearning.Core.Announcements;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ICommentDal _commentDal;
        private readonly IMapper _mapper;
        public CommentManager(IUnitOfWork unitOfWork, ICommentDal commentDal,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _commentDal = commentDal;
            _mapper = mapper;
        }

        public async Task<BasexResponse<Comment>> CreateAsync(Comment comment)
        {
            try
            {
                comment.CreationTime = DateTime.UtcNow;
                await _commentDal.AddAsync(comment);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<Comment>(comment);
            }
            catch (Exception ex)
            {
                return new BasexResponse<Comment>(ex.Message);
            }
        }

        public async Task<BasexResponse<Comment>> DeleteByIdAsync(int commentId)
        {
            try
            {
                var comment = await _commentDal.GetAsync(commentId);
                _commentDal.Delete(comment);
                await _unitOfWork.CompleteAsync();
                return new BasexResponse<Comment>(comment);
            }
            catch (Exception ex)
            {

                return new BasexResponse<Comment>(ex.Message);
            }
        }

        public async Task<BasexResponse<ICollection<CommentResponse>>> GetAllAsync(int announcementId)
        {
            try
            {
                var comments = await _commentDal.FindAllAsync(c => c.AnnouncementId == announcementId);
                var response = _mapper.Map<ICollection<Comment>, ICollection<CommentResponse>>(comments);
                return new BasexResponse<ICollection<CommentResponse>>(response);
            }
            catch (Exception ex)
            {

                return new BasexResponse<ICollection<CommentResponse>>(ex.Message);
            }
        }
    }
}
