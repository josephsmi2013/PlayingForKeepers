using PlayingForKeepers.Authorization;
using PlayingForKeepers.Models.DB;
using PlayingForKeepers.Models.DB.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

// dotnet aspnet-codegenerator razorpage -m Contact -dc ApplicationDbContext -udl -outDir Pages\Contacts --referenceScriptLibraries

namespace PlayingForKeepers.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new PlayingForKeepersDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<PlayingForKeepersDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Constants.LeaguesAdministratorRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.LeaguesManagerRole);

                SeedDB(context, adminID);
            }
        }


        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDB(PlayingForKeepersDbContext context, string adminID)
        {
            //if (context.FF_Leagues.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //context.FF_Leagues.AddRange(
            //    new FF_Leagues
            //    {                    
            //        LeagueName = "League ABC",
            //        LeagueTeamsUsed = 0,
            //        LeagueTeamsPossible = 10,
            //        LeagueOwnerID = adminID,
            //        LeagueStatus = LeagueStatusList.Approved

            //    }
            //    ,
            //    new FF_Leagues
            //    {
            //        LeagueName = "League DEF",
            //        LeagueTeamsUsed = 0,
            //        LeagueTeamsPossible = 12,
            //        LeagueOwnerID = adminID,
            //        LeagueStatus = LeagueStatusList.Approved

            //    }
                //,
                //new FF_Leagues
                //{
                //    LeagueName = "League JKL",
                //    LeagueTeamsUsed = 0,
                //    LeagueTeamsPossible = 10,
                //    LeagueOwnerID = adminID,
                //    LeagueStatus = LeagueStatusList.Rejected

                //},
                //new FF_Leagues
                //{
                //    LeagueName = "League MNO",
                //    LeagueTeamsUsed = 0,
                //    LeagueTeamsPossible = 10,
                //    LeagueOwnerID = adminID,
                //    LeagueStatus = LeagueStatusList.Submitted

                //}
                //);
            //context.SaveChanges();
        }

    }
}