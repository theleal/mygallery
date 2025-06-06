using APIGallery.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIGallery.Context
{
    public class ContextProject : DbContext
    {
        public ContextProject(DbContextOptions<ContextProject> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkArt> WorkArts { get; set; }
    }
}
