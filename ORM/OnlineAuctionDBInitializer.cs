using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Web;

namespace ORM
{
    public class OnlineAuctionDBInitializer : CreateDatabaseIfNotExists<EntityModelContext>
    {
        protected override void Seed(EntityModelContext context)
        {
            IList<Role> defaultRoles = new List<Role>();

            defaultRoles.Add(new Role() {RoleName = "Administrator"});
            defaultRoles.Add(new Role() { RoleName = "Moderator" });
            defaultRoles.Add(new Role() { RoleName = "User" });

            foreach (Role role in defaultRoles)
            {
                context.Set<Role>().Add(role);
            }

            var user = new User()
            {
                IsBlocked = false,
                CreationDate = DateTime.Now,
                BlockReason = "",
                BlockTime = DateTime.Now,
                Email = "admin@gmail.com",
                Login = "Admin94",
                Password = "notValid"
            };
            user.Roles.Add(defaultRoles[0]);


            context.Set<User>().Add(user);

            base.Seed(context);
        }
    }
}
