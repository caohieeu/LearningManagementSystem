using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.DAL
{
    public class SeedData
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedingData(LMSContext _context)
        {
            await _context.Database.MigrateAsync();

            if (!_context.Users.Any(x => x.UserName == "LeaderShip"))
            {
                var user = new ApplicationUser
                {
                    UserName = "LeaderShip",
                    FullName = "Quản lý",
                    Email = "leadership@gmail.com",
                    PhoneNumber = "0123456789",
                    Address = "LeaderShip",
                    Gender = "Male",
                    DepartmentId = "CNTT"
                };

                var result = await _userManager.CreateAsync(user, "123123@Admin");
                if (!result.Succeeded)
                {
                    return;
                }

                if (!await _roleManager.RoleExistsAsync(Utils.Roles.LeaderShip))
                {
                    await _roleManager.CreateAsync(new IdentityRole(Utils.Roles.LeaderShip));
                }

                await _userManager.AddToRoleAsync(user, Utils.Roles.LeaderShip);
            }
        }
    }
}
