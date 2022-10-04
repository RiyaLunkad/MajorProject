using CrimeMgmnt.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;

namespace CrimeMgmnt.Data
{
    /// <summary>
    /// A singleton Class; we can make singleton by adding static keyword.
    /// </summary>
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedIdentityRolesAsync(RoleManager<IdentityRole> rolemanager)
        {
            //this enumeration we are using to take the values from the Enum class Myidentityrolenames
            foreach (MyIdentityRoleNames rolename in Enum.GetValues(typeof(MyIdentityRoleNames)))
            {
                if (!await rolemanager.RoleExistsAsync(rolename.ToString()))
                {
                    await rolemanager.CreateAsync(
                        new IdentityRole { Name = rolename.ToString() });
                }
            }
        }

        public static async Task SeedIdentityUserAsync(UserManager<IdentityUser> usermanager)
        {
            IdentityUser user;

            user = await usermanager.FindByNameAsync("admin@demo.com");
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "admin@demo.com",
                    Email = "admin@demo.com",
                    EmailConfirmed = true
                };
                await usermanager.CreateAsync(user, password: "Password@123");

                await usermanager.AddToRolesAsync(user, new string[] {
                    MyIdentityRoleNames.AppAdmin.ToString(),
                      //MyIdentityRoleNames.AppUser.ToString()

                });
            }

            user = await usermanager.FindByNameAsync("user@demo.com");
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "user@demo.com",
                    Email = "user@demo.com",
                    EmailConfirmed = true
                };
                await usermanager.CreateAsync(user, password: "Asdf@123");
                //await usermanager.AddToRolesAsync(user, new string[] {
                //    MyIdentityRoleNames.AppUser.ToString()
                //});
                await usermanager.AddToRoleAsync(user, MyIdentityRoleNames.AppUser.ToString());
            }
        }

    }
}
