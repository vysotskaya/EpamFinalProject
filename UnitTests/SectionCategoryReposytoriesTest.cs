using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using DAL.Concrete;
using DAL.Interface.DTO;

namespace UnitTests
{
    [TestClass]
    public class SectionCategoryReposytoriesTest
    {
        private static readonly EntityModelContext Context = new EntityModelContext();
        private static readonly CategoryRepository CategoryRepository = new CategoryRepository(Context);
        private static readonly SectionRepository SectionRepository = new SectionRepository(Context, CategoryRepository);
        [TestMethod]
        public void CreateSection()
        {
            var section = new DalSection()
            {
                CreationDate = DateTime.Now,
                IsBlocked = false,
                SectionName = "Stars",
                UserRefId = 1
            };
            SectionRepository.Create(section);
            Context.SaveChanges();
        }
    }
}
