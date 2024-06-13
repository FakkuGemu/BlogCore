using Blog.DAL.Infrastructure;
using Blog.DAL.Repository;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Blog.DAL.Model;
using BlogCore.DAL.Tests;
using TDD.DbTestHelpers.Core;

namespace Blog.DAL.Tests
{

    [TestClass]
    public class RepositoryTests
    {
        public class TestDbTest : DbBaseTest<BlogFixtures>
        {
            public TestDbTest() : base() { }
        }

        [TestMethod]
        public void GetAllPost_TwoPostsInDb_ReturnTwoPosts()
        {
            // arrange
            String connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("BloggingDatabase");
            var repository = new BlogRepository(connectionString);
            TestDbTest dbTest = new TestDbTest();
            var context = dbTest.fixContext();
            context.Database.EnsureCreated();
            repository.ClearAll();
            // act
            var result = repository.GetAllPosts();
            // assert
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void GetAllPost_AddPost_ReturnPlusOne()
        {
            // arrange
            String connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("BloggingDatabase");
            var repository = new BlogRepository(connectionString);
            repository.ClearAll();
            // act
            var countBefore = repository.GetAllPosts().Count();
            repository.AddPost("test", "author");
            var countAfter = repository.GetAllPosts().Count();
            // assert
            Assert.AreEqual(countBefore + 1, countAfter);
        }
        [TestMethod]
        [ExpectedException(typeof(Microsoft.EntityFrameworkCore.DbUpdateException))]
        public void CantAddPost_WithEmptyAuthor()
        {
            String connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("BloggingDatabase");
            var repository = new BlogRepository(connectionString);
            repository.ClearAll();
            repository.AddPost("test", null);     
        }
        [TestMethod]
        [ExpectedException(typeof(Microsoft.EntityFrameworkCore.DbUpdateException))]
        public void CantAddPost_WithEmptyAuthorContent()
        {
            String connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("BloggingDatabase");
            var repository = new BlogRepository(connectionString);
            repository.ClearAll();
            repository.AddPost(null, "test");
        }
    }
}
