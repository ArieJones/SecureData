using ContactManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var uid = await CreateTestUser(serviceProvider);
                await CreateCanDeleteRole(serviceProvider, uid);
                SeedDB(context, uid);
            }
        }

        private static async Task<string> CreateTestUser(IServiceProvider serviceProvider)
        {
            // TO-DO move to secrets manager
            const string SeedUserName = "test@example.com";
            const string tmpPW = "Pa$$w0rd1!";
            
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(SeedUserName);
            if (user == null)
            {
                user = new ApplicationUser { UserName = SeedUserName };
                await userManager.CreateAsync(user, tmpPW);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> CreateCanDeleteRole(IServiceProvider serviceProvider, string uid)
        {
            const string canDeleteRole = "canDelete";
            IdentityResult ir = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(canDeleteRole))
            {
                ir = await roleManager.CreateAsync(new IdentityRole(canDeleteRole));
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByIdAsync(uid);
           
            ir = await userManager.AddToRoleAsync(user, canDeleteRole);
            return ir;
        }

        public static void SeedDB(ApplicationDbContext context, string uid)
        {
            if (context.Contact.Any())
            {
                return;   // DB has been seeded
            }

            context.Contact.AddRange(new Contact
            {
                Name = "Debra Garcia",
                Address = "1234 Main St",
                City = "Redmond",
                State = "WA",
                Zip = "10999",
                Email = "debra@example.com",
                 ApplicationUser_Id = uid
            },
    new Contact
    {
        Name = "Thorsten Weinrich",
        Address = "5678 1st Ave W",
        City = "Redmond",
        State = "WA",
        Zip = "10999",
        Email = "thorsten@example.com",
        ApplicationUser_Id = uid
    },
    new Contact
    {
        Name = "Yuhong Li",
        Address = "9012 State st",
        City = "Redmond",
        State = "WA",
        Zip = "10999",
        Email = "yuhong@example.com",
        ApplicationUser_Id = uid
    },
    new Contact
    {
        Name = "Jon Orton",
        Address = "3456 Maple St",
        City = "Redmond",
        State = "WA",
        Zip = "10999",
        Email = "jon@example.com",
        ApplicationUser_Id = uid
    },
    new Contact
    {
        Name = "Diliana Alexieva-Bosseva",
        Address = "7890 2nd Ave E",
        City = "Redmond",
        State = "WA",
        Zip = "10999",
        Email = "diliana@example.com",
        ApplicationUser_Id = uid
    }
    );
            context.SaveChanges();

        }
    }
}