using System.Collections.Generic;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using System;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Repository
{
    public class BlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository(string connectionString)
        {
            _context = new BlogContext(connectionString);
        }
        public void ClearAll()
        {
            _context.Posts.ExecuteDelete();
        }
        public void AddPost(string content, string author)
        {
            Post post = new Post {Content = content, Author = author };
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts;
        }
    }
}
