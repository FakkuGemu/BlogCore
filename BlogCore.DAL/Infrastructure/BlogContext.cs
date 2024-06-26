﻿using Blog.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Infrastructure
{
    public class BlogContext : DbContext
    {
        private string _defaultConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\tmp\\blog-test.mdf;Integrated Security=SSPI";

        private string _connectionString;
        public DbSet<Post> Posts { get; set; }


        public BlogContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }
        public BlogContext() : base()
        {
            _connectionString = _defaultConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
