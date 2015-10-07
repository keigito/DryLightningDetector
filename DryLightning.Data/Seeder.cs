using DryLightning.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryLightning.Data
{
    public class Seeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            RoleStore<Role> roleStore = new RoleStore<Role>(context);
            RoleManager<Role> roleManager = new RoleManager<Role>(roleStore);

            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new Role { Name = "Admin" });

            if (!roleManager.RoleExists("User"))
                roleManager.Create(new Role { Name = "User" });

            IdentityResult result = null;

            ApplicationUser user1 = userManager.FindByName("keigito@gmail.com");

            if (user1 == null)
            {
                user1 = new ApplicationUser { Email = "keigito@gmail.com", UserName = "keigito@gmail.com" };
            }

            result = userManager.Create(user1, "asdfasdf");
            if (!result.Succeeded)
            {
                string error = result.Errors.FirstOrDefault();
                throw new Exception(error);
            }

            userManager.AddToRole(user1.Id, "Admin");
            user1 = userManager.FindByName("keigito@gmail.com");

            ApplicationUser user2 = userManager.FindByName("keigito@yahoo.com");

            if (user2 == null)
            {
                user2 = new ApplicationUser { Email = "keigito@yahoo.com", UserName = "keigito@yahoo.com" };
            }

            result = userManager.Create(user2, "asdfasfd");
            if (!result.Succeeded)
            {
                string error = result.Errors.FirstOrDefault();
                throw new Exception(error);
            }

            userManager.AddToRole(user2.Id, "User");
            user2 = userManager.FindByName("keigito@yahoo.com");
        }
    }
}
