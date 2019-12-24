using EfLearning.Core.Practices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EfLearning.Data.Abstract
{
    public interface IDonePracticeDal: IRepository<DonePractice>
    {
        Task<DonePractice> UpdateAsync(DonePractice entity);
        Task<ICollection<DonePractice>>GetByUserIdWithGivenPracticeAsync(int userId);
    }
}
