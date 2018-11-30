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

        public virtual DbSet<ConsultasWatson> ConsultasWatson { get; set; }
        public virtual DbSet<ImagenMascota> ImagenMascota { get; set; }
        public virtual DbSet<Mascota> Mascota { get; set; }
        public virtual DbSet<Raza> Raza { get; set; }
        public virtual DbSet<RedSocial> RedSocial { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //datos produccion DB
                optionsBuilder.UseNpgsql("User ID=tuozypsimqmojk;Password=4efa38c2155e42ddfbf0b1a69b6ccb740f816f2e637dfa25fa13f1e777920aca;Server=ec2-50-19-249-121.compute-1.amazonaws.com;Port=5432;Database=d1tv94rvfp2pcl;Integrated Security=true;Pooling=true;");
                //optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Server=localhost;Port=5432;Database=PetFinderDB;Integrated Security=true;Pooling=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsultasWatson>(entity =>
            {
                entity.HasKey(e => e.IdConsulta);

                entity.Property(e => e.Clase).HasColumnType("character varying(25)");

                entity.HasOne(d => d.IdImagenNavigation)
                    .WithMany(p => p.ConsultasWatson)
                    .HasForeignKey(d => d.IdImagen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImagenMascota_ConsultasWatson");
            });

            modelBuilder.Entity<ImagenMascota>(entity =>
            {
                entity.HasKey(e => e.IdImagen);

                entity.Property(e => e.ImagenPath)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Localizacion).HasColumnType("character varying");

                entity.HasOne(d => d.IdMascotaNavigation)
                    .WithMany(p => p.ImagenMascota)
                    .HasForeignKey(d => d.IdMascota)
                    .HasConstraintName("ImagenMascota_IdMascota_fkey");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ImagenMascota)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ImagenMascota_IdUsuario_fkey");
            });

            modelBuilder.Entity<Mascota>(entity =>
            {
                entity.HasKey(e => e.IdMascota);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdRazaNavigation)
                    .WithMany(p => p.Mascota)
                    .HasForeignKey(d => d.IdRaza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Mascota_IdRaza_fkey");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Mascota)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Mascota_IdUsuario_fkey");
            });

            modelBuilder.Entity<Raza>(entity =>
            {
                entity.HasKey(e => e.IdRaza);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<RedSocial>(entity =>
            {
                entity.HasKey(e => e.IdRedSocial);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.Avatar).HasColumnType("character varying");

                entity.Property(e => e.Direccion).HasColumnType("character varying");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.IdUsuarioRedSocial).HasColumnType("character varying");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.TelefonoContacto).HasColumnType("character varying");

                entity.HasOne(d => d.IdRedSocialNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdRedSocial)
                    .HasConstraintName("Usuario_IdRedSocial_fkey");
            });
        }
    }
}
