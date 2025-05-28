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

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Obra> Obras { get; set; }
    }
}
