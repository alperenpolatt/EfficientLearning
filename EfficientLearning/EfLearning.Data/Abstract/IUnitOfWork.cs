using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Abstract
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();

        void Complete();
    }
}
