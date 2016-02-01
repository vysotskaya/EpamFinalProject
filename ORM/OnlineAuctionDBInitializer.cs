using System;
using System.Collections.Generic;
using System.Data.Entity;

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

            base.Seed(context);
        }
    }
}
