﻿using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.Clientes;
using Prestamos.Core.Entidades.Configuraciones;
using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Infraestructure.TEMP;

namespace Prestamos.Infraestructure.Contexto;

public partial class PrestamoContext : DbContext
{
    public PrestamoContext()
    {
    }

    public PrestamoContext(DbContextOptions<PrestamoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acesor> Acesor { get; set; }

    public virtual DbSet<Ciudad> Ciudad { get; set; }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<Configuracion> Configuracion { get; set; }

    public virtual DbSet<DocumentoTipo> DocumentoTipo { get; set; }

    public virtual DbSet<FormaPago> FormaPago { get; set; }

    public virtual DbSet<FormaPagoFecha> FormaPagoFecha { get; set; }

    public virtual DbSet<MetodoPago> MetodoPago { get; set; }

    public virtual DbSet<Moneda> Moneda { get; set; }

    public virtual DbSet<Ocupacion> Ocupacion { get; set; }

    public virtual DbSet<Permiso> Permiso { get; set; }

    public virtual DbSet<Prestamo> Prestamo { get; set; }

    public virtual DbSet<PrestamoCuota> PrestamoCuota { get; set; }

    public virtual DbSet<PrestamoEstado> PrestamoEstado { get; set; }

    public virtual DbSet<PrestamoPago> PrestamoPago { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Sexo> Sexo { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<VwCliente> VwCliente { get; set; }

    public virtual DbSet<VwPrestamo> VwPrestamo { get; set; }

    public virtual DbSet<VwPrestamoCuota> VwPrestamoCuota { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasOne(d => d.Ciudad).WithMany(p => p.Cliente).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DocumentoTipo).WithMany(p => p.Cliente).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Ocupacion).WithMany(p => p.Cliente).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Sexo).WithMany(p => p.Cliente).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Usuario).WithMany(p => p.ClienteUsuario).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<FormaPagoFecha>(entity =>
        {
            entity.HasOne(d => d.FormaPago).WithMany(p => p.FormaPagoFecha).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasOne(d => d.Rol).WithMany(p => p.Permiso).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasOne(d => d.Cliente).WithMany(p => p.Prestamo).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Estado).WithMany(p => p.Prestamo).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.FormaPago).WithMany(p => p.Prestamo).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.MetodoPago).WithMany(p => p.Prestamo).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Moneda).WithMany(p => p.Prestamo).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Usuario).WithMany(p => p.PrestamoUsuario).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UsuarioIdActualizadoNavigation).WithMany(p => p.PrestamoUsuarioIdActualizadoNavigation).HasConstraintName("FK_Prestamo_Usuario_ActualizadoUsuarioId");
        });

        modelBuilder.Entity<PrestamoCuota>(entity =>
        {
            entity.HasOne(d => d.Prestamo).WithMany(p => p.PrestamoCuota).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PrestamoPago>(entity =>
        {
            entity.HasOne(d => d.MetodoPago).WithMany(p => p.PrestamoPago).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PrestamoCuota).WithMany(p => p.PrestamoPago).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Usuario).WithMany(p => p.PrestamoPagoUsuario).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasOne(d => d.Rol).WithMany(p => p.Usuario).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<VwCliente>(entity =>
        {
            entity.ToView("VwCliente");
        });

        modelBuilder.Entity<VwPrestamo>(entity =>
        {
            entity.ToView("VwPrestamo");
        });

        modelBuilder.Entity<VwPrestamoCuota>(entity =>
        {
            entity.ToView("VwPrestamoCuota");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
