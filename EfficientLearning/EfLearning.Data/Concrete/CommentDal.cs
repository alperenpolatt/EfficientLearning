using EfLearning.Core.Announcements;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Data.Concrete
{
    public class CommentDal:BaseRepository<Comment>,ICommentDal
    {
        public CommentDal(EfContext context) : base(context)
        {
        }
    }
}
