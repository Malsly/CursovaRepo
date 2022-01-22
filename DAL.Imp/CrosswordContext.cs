using DAL.Abs;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity;

namespace DAL.Impl
{
    public class CrosswordContext : System.Data.Entity.DbContext
    {
        public CrosswordContext()
        : base()
        {
        }

        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CrosswordDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(conn);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThemeAndWords>()
            .HasMany<Word>(s => s.Words)
            .WithRequired(g => g.ThemeAndWords)
            .HasForeignKey<int?>(s => s.ThemeAndWordsID)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Word>()
            .HasRequired<ThemeAndWords>(p => p.ThemeAndWords)
            .WithMany(t=>t.Words)
            .HasForeignKey(k=>k.ThemeAndWordsID)
            .WillCascadeOnDelete(false);
        }

        public Microsoft.EntityFrameworkCore.DbSet<ThemeAndWords> ThemeAndWords { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Word> Words { get; set; }
    }
}