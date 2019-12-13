using EfLearning.Core.Classrooms;
using EfLearning.Core.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EfLearning.Data.Concrete
{
    public class SeedDatabase
    {
        public static async void Seed()
        {
            var context = new EfContext();

            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Roles.Count() == 0)
                {
                    context.Roles.AddRange(Roles);
                }
                await context.SaveChangesAsync();
            }
        }

        private static AppRole[] Roles = {
            new AppRole(){ Name=CustomRoles.Admin,NormalizedName=CustomRoles.Admin.ToUpperInvariant()},
            new AppRole(){ Name=CustomRoles.Teacher,NormalizedName=CustomRoles.Teacher.ToUpperInvariant()},
            new AppRole(){ Name=CustomRoles.Student,NormalizedName=CustomRoles.Student.ToUpperInvariant()}
        };

    }
}
