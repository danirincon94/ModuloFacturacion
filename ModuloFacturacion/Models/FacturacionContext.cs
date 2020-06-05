using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ModuloFacturacion.Models
{
    public partial class FacturacionContext : DbContext
    {
        public FacturacionContext()
        {
        }

        public FacturacionContext(DbContextOptions<FacturacionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:integracion.database.windows.net,1433;Initial Catalog=integracion;Persist Security Info=False;User ID=intergrupo;Password=Colombia20;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.Property(e => e.IdCliente)
                    .HasColumnName("idCliente")
                    .ValueGeneratedNever();

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fechaNacimiento")
                    .HasColumnType("datetime");

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasColumnName("nombreCliente")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasColumnName("telefono")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.IdDetalleFactura);

                entity.Property(e => e.IdDetalleFactura)
                    .HasColumnName("idDetalleFactura")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdFactura).HasColumnName("idFactura");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.ValorTotal)
                    .HasColumnName("valorTotal")
                    .HasColumnType("money");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.DetalleFactura)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleFactura_Factura");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetalleFactura)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleFactura_Producto");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura);

                entity.Property(e => e.IdFactura)
                    .HasColumnName("idFactura")
                    .ValueGeneratedNever();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fechaCreacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.ValorTotal)
                    .HasColumnName("valorTotal")
                    .HasColumnType("money");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Factura_Cliente");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.Property(e => e.IdProducto)
                    .HasColumnName("idProducto")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValorUnitario)
                    .HasColumnName("valorUnitario")
                    .HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
