using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(CoreDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            // add default branch
            var branchCount = await context.Branches.CountAsync();
            if (branchCount == 0)
            {
                context.Branches.Add(new Domain.Entity.Branch
                {
                    BranchName = "Main Branch",
                    CreatedBy = "System",
                    LastModified = DateTime.UtcNow,
                    ModifiedBy = "System",
                    BranchCreditObjectId = Guid.NewGuid(),
                    BranchBonusObjectId = Guid.NewGuid()
                });
                await context.SaveChangesAsync();
            }

            // add default usertypes
            var userTypeCount = await context.UserTypes.CountAsync();
            if (userTypeCount == 0)
            {
                List<UserType> usertypes = new List<UserType>();

                usertypes.Add(new UserType { UserTypeId = 1, UserTypeName = "Super Admin", GroupType = 0, RoleType = 0 });
                usertypes.Add(new UserType { UserTypeId = 2, UserTypeName = "Operator", GroupType = 0, RoleType = 0 });
                usertypes.Add(new UserType { UserTypeId = 3, UserTypeName = "Game Site Manager", GroupType = 0, RoleType = 2 });
                usertypes.Add(new UserType { UserTypeId = 4, UserTypeName = "Recruiter", GroupType = 0, RoleType = 2 });
                usertypes.Add(new UserType { UserTypeId = 5, UserTypeName = "Player", GroupType = 0, RoleType = -1 });
                usertypes.Add(new UserType { UserTypeId = 6, UserTypeName = "NewRegister", GroupType = 0, RoleType = -1 });

                context.UserTypes.AddRange(usertypes);
                await context.SaveChangesAsync();
            }

            //// add default value for branch bonus object id
            //var branches = await context.Branches.ToListAsync();
            //for (int i = 0; i < branches.Count; i++)
            //{
            //    var branch = branches[i];
            //    if (branch.BranchBonusObjectId == null)
            //    {
            //        branch.BranchBonusObjectId = Guid.NewGuid();
            //        await context.SaveChangesAsync();
            //    }
            //}
        }
    }
}
