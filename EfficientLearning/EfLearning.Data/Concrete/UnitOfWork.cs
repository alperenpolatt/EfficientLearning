using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EfContext context;

        public UnitOfWork(EfContext context)
        {
            this.context = context;
        }

        public void Complete()
        {
            this.context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
