namespace BDVendaDireta.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BDVendaDiretaContect : DbContext
    {
        public BDVendaDiretaContect()
            : base("name=BDVendaDiretaContect")
        {
        }

        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Produto>()
                .Property(e => e.Preco)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Senha)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Receita)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Produto)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);
        }
    }
}
