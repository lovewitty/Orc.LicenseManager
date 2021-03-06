﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="Orchestra development team">
//   Copyright (c) 2008 - 2014 Orchestra development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.LicenseManager.Server.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catel.Data;
    using Catel.IoC;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class AccountService : IAccountService
    {
        #region Methods
 

        public void CreateUserWithRoles(string userName, string password, List<string> userRoles)
        {
            if (!userRoles.Any())
            {
                throw new Exception("No roles were given to the user.", new Exception("To create a User you will have to give atleast 1 role to him."));
            }

            userRoles.ToList().ForEach(role =>
            {
                if (RoleExists(role) == false)
                {
                    throw new Exception("Only created roles can be assigned to Users.", new Exception("The role \"" + role + "\" doesn't exist."));
                }
            });
            using (var dbContextManager = DbContextManager<LicenseManagerDbContext>.GetManager())
            {
                var userManager = new UserManager<User>(new UserStore<User>(dbContextManager.Context));
                var user = new User {UserName = userName};
                userManager.Create(user, password);
                userRoles.ToList().ForEach(x => userManager.AddToRole(user.Id, x));
            }
        }

        public bool RoleExists(string rolestr)
        {
            using (var dbContextManager = DbContextManager<LicenseManagerDbContext>.GetManager())
            {
                var roleManager = new RoleManager<Role>(new RoleStore<Role>(dbContextManager.Context));
                var role = roleManager.FindByName(rolestr);
                if (role != null)
                {
                    return true;
                }
                return false;
            }
        }

        public void CreateRole(string role)
        {
            using (var dbContextManager = DbContextManager<LicenseManagerDbContext>.GetManager())
            {
                var roleManager = new RoleManager<Role>(new RoleStore<Role>(dbContextManager.Context));
                roleManager.Create(new Role(role));
            }
        }
        #endregion
    }
}