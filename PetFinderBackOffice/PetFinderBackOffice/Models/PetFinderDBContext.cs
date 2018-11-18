using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PetFinderBackOffice.Models
{
    public partial class PetFinderDBContext : DbContext
    {
        public PetFinderDBContext()
        {
        }

        public PetFinderDBContext(DbContextOptions<PetFinderDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ImagenMascota> ImagenMascota { get; set; }
        public virtual DbSet<Mascota> Mascota { get; set; }
        public virtual DbSet<Raza> Raza { get; set; }
        public virtual DbSet<RedSocial> RedSocial { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PetFinderDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImagenMascota>(entity =>
            {
                entity.HasKey(e => e.IdImagen);

                entity.Property(e => e.ImagenPath)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Localizacion).IsUnicode(false);

                entity.HasOne(d => d.IdMascotaNavigation)
                    .WithMany(p => p.ImagenMascota)
                    .HasForeignKey(d => d.IdMascota)
                    .HasConstraintName("FK__ImagenMas__IdMas__2B3F6F97");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ImagenMascota)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImagenMascota_Usuario");
            });

            modelBuilder.Entity<Mascota>(entity =>
            {
                entity.HasKey(e => e.IdMascota);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRazaNavigation)
                    .WithMany(p => p.Mascota)
                    .HasForeignKey(d => d.IdRaza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mascota__IdRaza__286302EC");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Mascota)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mascota__IdUsuar__276EDEB3");
            });

            modelBuilder.Entity<Raza>(entity =>
            {
                entity.HasKey(e => e.IdRaza);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RedSocial>(entity =>
            {
                entity.HasKey(e => e.IdRedSocial);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.IdUsuario).ValueGeneratedOnAdd();

                entity.Property(e => e.Avatar).IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdUsuarioRedSocial)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoContacto)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithOne(p => p.InverseIdUsuarioNavigation)
                    .HasForeignKey<Usuario>(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_RedSocial");
            });
        }
    }
}
