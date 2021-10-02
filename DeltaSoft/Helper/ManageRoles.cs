using Microsoft.AspNetCore.Identity;
using DeltaSoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaSoft.Helper
{
    sealed public class ManageRoles
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;


        public ManageRoles(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }



        // add to  employee

        public async Task<bool> AddToEmployeeRole(ApplicationUser user)
        {
            try
            {
                if (!await roleManager.RoleExistsAsync("Employee"))
                    await roleManager.CreateAsync(new IdentityRole("Employee"));

                if (await roleManager.RoleExistsAsync("Employee"))
                {
                    await userManager.AddToRoleAsync(user, "Employee");

                }
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }


        // add to  admin

        public async Task<bool> AddToSAdminRole(ApplicationUser user)
        {
            try
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(new IdentityRole("Admin"));

                if (await roleManager.RoleExistsAsync("Admin"))
                {
                    await userManager.AddToRoleAsync(user, "Admin");

                }
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
