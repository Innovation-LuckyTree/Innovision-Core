using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(CoreDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            //// add default value for company bonus object id
            //var companies = await context.Companies.ToListAsync();
            //for (int i = 0; i < companies.Count; i++)
            //{
            //    var company = companies[i];
            //    if (company.CompanyBonusObjectId == null) {
            //        company.CompanyBonusObjectId = Guid.NewGuid();
            //        await context.SaveChangesAsync();
            //    }
            //}

            // add default value for branch bonus object id
            var branches = await context.Branches.ToListAsync();
            for (int i = 0; i < branches.Count; i++)
            {
                var branch = branches[i];
                if (branch.BranchBonusObjectId == null)
                {
                    branch.BranchBonusObjectId = Guid.NewGuid();
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
