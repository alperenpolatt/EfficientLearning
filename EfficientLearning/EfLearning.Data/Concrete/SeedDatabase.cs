using EfLearning.Core.Classrooms;
using EfLearning.Core.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EfLearning.Data.Concrete
{
    public class SeedDatabase
    {
        public static void Seed()
        {
            var context = new EfContext();

            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Roles.Count() == 0)
                {
                    context.Roles.AddRange(Roles);
                }
                context.SaveChanges();
            }
        }

        private static AppRole[] Roles = {
            new AppRole(){ Name=CustomRoles.Admin},
            new AppRole(){ Name=CustomRoles.Teacher},
            new AppRole(){ Name=CustomRoles.Student}
        };

    }
}
