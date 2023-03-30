using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PW_Proyecto.Models;

public partial class TorneoappContext : DbContext
{
    public TorneoappContext()
    {
    }

    public TorneoappContext(DbContextOptions<TorneoappContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Partido> Partidos { get; set; }

    public virtual DbSet<Torneo> Torneos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=127.0.0.1;userid=root;password=admin;database=torneoapp;TreatTinyAsBoolean=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Partido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("partido");

            entity.HasIndex(e => e.Jugador1Id, "jugador1_id");

            entity.HasIndex(e => e.Jugador2Id, "jugador2_id");

            entity.HasIndex(e => e.TorneoId, "torneo_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Jugador1Id).HasColumnName("jugador1_id");
            entity.Property(e => e.Jugador2Id).HasColumnName("jugador2_id");
            entity.Property(e => e.TorneoId).HasColumnName("torneo_id");

            entity.HasOne(d => d.Jugador1).WithMany(p => p.PartidoJugador1s)
                .HasForeignKey(d => d.Jugador1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partido_ibfk_2");

            entity.HasOne(d => d.Jugador2).WithMany(p => p.PartidoJugador2s)
                .HasForeignKey(d => d.Jugador2Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partido_ibfk_3");

            entity.HasOne(d => d.Torneo).WithMany(p => p.Partidos)
                .HasForeignKey(d => d.TorneoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("partido_ibfk_1");
        });

        modelBuilder.Entity<Torneo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("torneo");

            entity.HasIndex(e => e.Organizador, "organizador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("date")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.MaxParticipantes).HasColumnName("max_participantes");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Organizador).HasColumnName("organizador");

            entity.HasOne(d => d.OrganizadorNavigation).WithMany(p => p.TorneosNavigation)
                .HasForeignKey(d => d.Organizador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("torneo_ibfk_1");

            entity.HasMany(d => d.Users).WithMany(p => p.Torneos)
                .UsingEntity<Dictionary<string, object>>(
                    "Participante",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("participantes_ibfk_2"),
                    l => l.HasOne<Torneo>().WithMany()
                        .HasForeignKey("TorneoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("participantes_ibfk_1"),
                    j =>
                    {
                        j.HasKey("TorneoId", "UserId").HasName("PRIMARY");
                        j.ToTable("participantes");
                        j.HasIndex(new[] { "UserId" }, "user_id");
                        j.IndexerProperty<int>("TorneoId").HasColumnName("torneo_id");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
