using System;
using System.Collections.Generic;
using EmployeesWebApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesWebApp.Models;

public partial class EmpleadosDbContext : DbContext
{
    public EmpleadosDbContext()
    {
    }

    public EmpleadosDbContext(DbContextOptions<EmpleadosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Puesto> Puestos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HELENRC; Database=EmpleadosDB; Trusted_Connection=True; Integrated Security=true; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PK__Empleado__958BE91044985BF8");

            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaContratacion).HasColumnType("date");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Fotografia).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Estado).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK_EstadoId");

            entity.HasOne(d => d.Puesto).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.PuestoId)
                .HasConstraintName("FK_PuestoId");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.EstadoId).HasName("PK__Estado__FEF86B00B28DA214");

            entity.ToTable("Estado");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Puesto>(entity =>
        {
            entity.HasKey(e => e.PuestoId).HasName("PK__Puesto__F7F6C60456E50368");

            entity.ToTable("Puesto");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
