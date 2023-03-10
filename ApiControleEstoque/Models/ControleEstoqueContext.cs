using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ApiControleEstoque.Models
{
    public partial class ControleEstoqueContext : DbContext
    {
        public ControleEstoqueContext() { }

        public ControleEstoqueContext(DbContextOptions<ControleEstoqueContext> options) : base(options) { }

        public virtual DbSet<Estoque> Estoque { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Estoque>(entity => 
            {
                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Quantidade).IsUnicode(false);
            });
        }
    }
}
