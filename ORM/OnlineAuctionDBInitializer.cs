﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Helpers;

namespace ORM
{
    public class OnlineAuctionDBInitializer : CreateDatabaseIfNotExists<EntityModelContext>
    {
        protected override void Seed(EntityModelContext context)
        {
            IList<Role> defaultRoles = new List<Role>();

            defaultRoles.Add(new Role() { RoleName = "Administrator"});
            defaultRoles.Add(new Role() { RoleName = "Moderator" });
            defaultRoles.Add(new Role() { RoleName = "User" });

            foreach (Role role in defaultRoles)
            {
                context.Set<Role>().Add(role);
            }

            var admin = new User()
            {
                IsBlocked = false,
                CreationDate = DateTime.Now,
                BlockReason = "",
                BlockTime = DateTime.Now,
                Email = "admin@gmail.com",
                Login = "Admin",
                Password = Crypto.HashPassword("admin1234")
            };
            admin.Roles.Add(defaultRoles[0]);
            //admin.Roles.Add(defaultRoles[2]);

            var user = new User()
            {
                IsBlocked = false,
                CreationDate = DateTime.Now,
                BlockReason = "",
                BlockTime = DateTime.Now,
                Email = "user@gmail.com",
                Login = "User",
                Password = Crypto.HashPassword("user1234")
            };
            //ser.Roles.Add(defaultRoles[1]);
            user.Roles.Add(defaultRoles[1]);

            //admin.Roles.Add(defaultRoles[2]);
            //user.Roles.Add(defaultRoles[2]);

            context.Set<User>().Add(admin);
            context.Set<User>().Add(user);

            //var section = new Section()
            //{
            //    CreationDate = DateTime.Now,
            //    Discription = "Section1 discription",
            //    IsBlocked = false,
            //    SectionId = 1,
            //    SectionName = "Section1",
            //    UserRefId = 2
            //};

            //context.Set<Section>().Add(section);

            //var category = new Category()
            //{
            //    CategoryId = 1,
            //    CategoryName = "Category1",
            //    CreationDate = DateTime.Now,
            //    Discription = "Category1 discription",
            //    IsBlocked = false,
            //    IsConfirmed = true,
            //    SectionRefId = 1
            //};

            //context.Set<Category>().Add(category);

            base.Seed(context);
        }
    }
}
