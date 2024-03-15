using CommentDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentDataAccess
{
    public class CommentContext : DbContext
    {
        public CommentContext(DbContextOptions<CommentContext> option) : 
            base(option)
        {

        }

        public DbSet<Commentaire> Commentaires { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Commentaire>()
                .HasIndex(commentaire => commentaire.IdComment);

            base.OnModelCreating(modelBuilder);
        }
    }

}
