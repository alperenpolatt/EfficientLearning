using EfLearning.Core.Classrooms;
using EfLearning.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfLearning.Data.Concrete
{
    public class MaterialDal: BaseRepository<Material>, IMaterialDal
    {
        public MaterialDal(EfContext context) : base(context)
        {
        }
    }
}
