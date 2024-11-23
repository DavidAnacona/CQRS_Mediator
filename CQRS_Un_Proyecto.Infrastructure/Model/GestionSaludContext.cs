using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace CQRS_Un_Proyecto.Infrastructure.Model;

public partial class GestionSaludContext : DbContext
{
    public GestionSaludContext()
    {
    }

    public GestionSaludContext(DbContextOptions<GestionSaludContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Consultum> Consulta { get; set; }

    public virtual DbSet<EstadoConsultum> EstadoConsulta { get; set; }

    public virtual DbSet<HistorialEstadoConsultum> HistorialEstadoConsulta { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Recetum> Receta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Consultum>(entity =>
        {
            entity.HasKey(e => e.IdConsulta).HasName("PRIMARY");

            entity.HasIndex(e => e.IdEstadoConsulta, "id_estadoConsulta");

            entity.HasIndex(e => e.IdMedico, "id_medico");

            entity.HasIndex(e => e.IdPaciente, "id_paciente");

            entity.Property(e => e.IdConsulta).HasColumnName("id_consulta");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.IdEstadoConsulta).HasColumnName("id_estadoConsulta");
            entity.Property(e => e.IdMedico).HasColumnName("id_medico");
            entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");
            entity.Property(e => e.Notas)
                .HasColumnType("text")
                .HasColumnName("notas");

            entity.HasOne(d => d.IdEstadoConsultaNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.IdEstadoConsulta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Consulta_ibfk_3");

            entity.HasOne(d => d.IdMedicoNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.IdMedico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Consulta_ibfk_2");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Consulta_ibfk_1");
        });

        modelBuilder.Entity<EstadoConsultum>(entity =>
        {
            entity.HasKey(e => e.IdEstadoConsulta).HasName("PRIMARY");

            entity.HasIndex(e => e.NombreEstado, "nombre_estado").IsUnique();

            entity.Property(e => e.IdEstadoConsulta).HasColumnName("id_estadoConsulta");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(50)
                .HasColumnName("nombre_estado");
        });

        modelBuilder.Entity<HistorialEstadoConsultum>(entity =>
        {
            entity.HasKey(e => e.IdHistorialEstado).HasName("PRIMARY");

            entity.HasIndex(e => e.IdConsulta, "id_consulta");

            entity.HasIndex(e => e.IdEstadoConsulta, "id_estadoConsulta");

            entity.Property(e => e.IdHistorialEstado).HasColumnName("id_historialEstado");
            entity.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.FechaCambio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_cambio");
            entity.Property(e => e.IdConsulta).HasColumnName("id_consulta");
            entity.Property(e => e.IdEstadoConsulta).HasColumnName("id_estadoConsulta");
            entity.Property(e => e.UsuarioResponsable)
                .HasMaxLength(100)
                .HasColumnName("usuario_responsable");

            entity.HasOne(d => d.IdConsultaNavigation).WithMany(p => p.HistorialEstadoConsulta)
                .HasForeignKey(d => d.IdConsulta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("HistorialEstadoConsulta_ibfk_1");

            entity.HasOne(d => d.IdEstadoConsultaNavigation).WithMany(p => p.HistorialEstadoConsulta)
                .HasForeignKey(d => d.IdEstadoConsulta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("HistorialEstadoConsulta_ibfk_2");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.HasKey(e => e.IdMedico).HasName("PRIMARY");

            entity.ToTable("Medico");

            entity.Property(e => e.IdMedico).HasColumnName("id_medico");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(100)
                .HasColumnName("especialidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PRIMARY");

            entity.ToTable("Paciente");

            entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("datetime")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Recetum>(entity =>
        {
            entity.HasKey(e => e.IdReceta).HasName("PRIMARY");

            entity.HasIndex(e => e.IdConsulta, "id_consulta");

            entity.Property(e => e.IdReceta).HasColumnName("id_receta");
            entity.Property(e => e.IdConsulta).HasColumnName("id_consulta");
            entity.Property(e => e.Indicaciones)
                .HasColumnType("text")
                .HasColumnName("indicaciones");
            entity.Property(e => e.Medicamentos)
                .HasColumnType("text")
                .HasColumnName("medicamentos");

            entity.HasOne(d => d.IdConsultaNavigation).WithMany(p => p.Receta)
                .HasForeignKey(d => d.IdConsulta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Receta_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
