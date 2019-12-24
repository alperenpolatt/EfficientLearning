using EfLearning.Core.Practices;
using EfLearning.Data.Abstract;

namespace EfLearning.Data.Concrete
{
    public class GivenPracticeDal : BaseRepository<GivenPractice>, IGivenPracticeDal
    {
        public GivenPracticeDal(EfContext context) : base(context)
        {
        }
    }
}
