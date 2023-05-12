using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessLayer;
using System.ComponentModel;

namespace TestingLayer
{
    [TestFixture]
    public class CategoryContextTest
    {
        private CategoryContext context = new(SetupFixture.dbContext);
        private Category category;
        private User user1;
        private User user2;

        [SetUp]
        public void SetUp() 
        {
            category  = new("pinball");
            user1 = new("joro", "bekama", 23, "joro1606", "jorov3lik", "joro9@abv.bg");
            user2 = new("martin", "kompirov", 28, "marto1606", "marov3lik", "maro3@abv.bg");
            category.Users.Add(user1);
            category.Users.Add(user2);

            context.Create(category);
        }

        [TearDown]
        public void DropCupboards()
        {
            foreach (Category item in SetupFixture.dbContext.Categories.ToList())
            {
                SetupFixture.dbContext.Categories.Remove(item);
            }

            SetupFixture.dbContext.SaveChanges();
        }


        [Test]
        public void Create()
        {
            Category testCategory = new("crochet");

            int categoriesBefore = SetupFixture.dbContext.Categories.Count();

            context.Create(testCategory);

            int categoriesAfter = SetupFixture.dbContext.Categories.Count();

            Assert.That(categoriesBefore + 1 == categoriesAfter, "Create() does not work!");
        }

        [Test]
        public void Read()
        {
            Category readCategory = context.Read(category.Id);

            Assert.AreEqual(category, readCategory, "Read does not return the same object!");
        }

        [Test]
        public void ReadWithNavigationalProperties()
        {
            Category readCategory = context.Read(category.Id, true);

            Assert.That(readCategory.Users.Contains(user1)
                && readCategory.Users.Contains(user1),
                "user1 and user2 are not in the Users list!");
        }

        [Test]
        public void ReadAll()
        {
            List<Category> categories = (List<Category>)context.ReadAll();

            Assert.That(categories.Count != 0, "ReadAll() does not return categories!");
        }

        [Test]
        public void ReadAllWithNavigationalProperties()
        {
            Category readCategory = new("frisbi");
            User user1 = new("joko", "bekamcheto", 32, "joro1707", "jorov8lik", "joro24@abv.bg");
            SetupFixture.dbContext.Users.Add(user1);
            SetupFixture.dbContext.Categories.Add(readCategory);
            context.Create(readCategory);

            List<Category> categories = (List<Category>)context.ReadAll(true);

            Assert.That(categories.Count != 0
                && context.Read(readCategory.Id, true).Users.Count != 1, "ReadAll() does not return categories!");
        }

        [Test]
        public void Update()
        {
            Category changedCategory = context.Read(category.Id);

            changedCategory.Name = "e34";

            context.Update(changedCategory);

            Assert.AreEqual(changedCategory, category, "Update() does not work!");
        }

        //[Test]
        //public void UpdateWithNavigationalProperties()
        //{
        //	//finish
        //}

        [Test]
        public void Delete()
        {
            int cupboardsBefore = SetupFixture.dbContext.Categories.Count();

            context.Delete(category.Id);
            int cupboardsAfter = SetupFixture.dbContext.Categories.Count();

            Assert.IsTrue(cupboardsBefore - 1 == cupboardsAfter, "Delete() does not work!");
        }
    }
}
