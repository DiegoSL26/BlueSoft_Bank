using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bluesoft_Bank.Models;

public partial class BankContext : DbContext
{
    public BankContext()
    {
    }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3213E83FA55C3648");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(15)
                .HasColumnName("tipoCliente");
            entity.Property(e => e.TransaccionId).HasColumnName("transaccionID");

            entity.HasOne(d => d.Transaccion).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.TransaccionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Cliente__transac__4316F928");
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuenta__3213E83F9AAC8455");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.ClientId).HasColumnName("clientID");
            entity.Property(e => e.Saldo).HasColumnName("saldo");
            entity.Property(e => e.TipoCuenta)
                .HasMaxLength(10)
                .HasColumnName("tipoCuenta");
            entity.Property(e => e.TransaccionId).HasColumnName("transaccionID");

            entity.HasOne(d => d.Client).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Cuenta__clientID__46E78A0C");

            entity.HasOne(d => d.Transaccion).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.TransaccionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Cuenta__transacc__47DBAE45");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transacc__3213E83F4A11F577");

            entity.ToTable("Transaccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CiudadOrigen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ciudadOrigen");
            entity.Property(e => e.TipoTransaccion)
                .HasMaxLength(15)
                .HasColumnName("tipoTransaccion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
