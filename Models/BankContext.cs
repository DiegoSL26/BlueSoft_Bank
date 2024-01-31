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

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admin__3213E83F7F77E274");

            entity.ToTable("Admin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3213E83F486ACAF0");

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
                .HasMaxLength(10)
                .HasColumnName("tipoCliente");
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cuenta__3213E83F3E36BC51");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.ClienteId).HasColumnName("clienteID");
            entity.Property(e => e.Saldo).HasColumnName("saldo");
            entity.Property(e => e.TipoCuenta)
                .HasMaxLength(10)
                .HasColumnName("tipoCuenta");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cuentas)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Cuenta__clienteI__245D67DE");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transacc__3213E83F2655C237");

            entity.ToTable("Transaccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CiudadOrigen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ciudadOrigen");
            entity.Property(e => e.ClienteId).HasColumnName("clienteID");
            entity.Property(e => e.CuentaId).HasColumnName("cuentaID");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Monto).HasColumnName("monto");
            entity.Property(e => e.TipoTransaccion)
                .HasMaxLength(15)
                .HasColumnName("tipoTransaccion");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Transacci__clien__2DE6D218");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.CuentaId)
                .HasConstraintName("FK__Transacci__cuent__2CF2ADDF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
