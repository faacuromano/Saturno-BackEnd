using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Data;

public partial class SaturnoV2Context : DbContext
{
    public SaturnoV2Context()
    {
    }

    public SaturnoV2Context(DbContextOptions<SaturnoV2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrador> Administradors { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Profesionale> Profesionales { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Administ__3214EC27B86D3D47");

            entity.ToTable("Administrador");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreacionCuenta)
                .HasColumnType("smalldatetime")
                .HasColumnName("creacionCuenta");
            entity.Property(e => e.Passw)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("passw");
            entity.Property(e => e.Semilla)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("semilla");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdUsuarios).HasName("PK__Clientes__3214EC2778E80F19");

            entity.Property(e => e.IdUsuarios)
                .ValueGeneratedNever()
                .HasColumnName("ID_Usuarios");

            entity.HasOne(d => d.IdUsuariosNavigation).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.IdUsuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Usuarios");
        });

        modelBuilder.Entity<Profesionale>(entity =>
        {
            entity.HasKey(e => e.IdUsuarios).HasName("PK__Profesio__3214EC27AEACA9D8");

            entity.Property(e => e.IdUsuarios)
                .ValueGeneratedNever()
                .HasColumnName("ID_Usuarios");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.EstadoSub)
                .HasDefaultValueSql("((0))")
                .HasColumnName("estadoSub");
            entity.Property(e => e.FotoBanner)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasDefaultValueSql("('https://s1.1zoom.me/big0/781/Austria_Mountains_Lake_Tyrol_Alps_Clouds_597871_1280x768.jpg')")
                .HasColumnName("fotoBanner");
            entity.Property(e => e.HorarioFinal)
                .HasPrecision(0)
                .HasColumnName("horarioFinal");
            entity.Property(e => e.HorarioInicio)
                .HasPrecision(0)
                .HasColumnName("horarioInicio");
            entity.Property(e => e.Profesion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("profesion");

            entity.HasOne(d => d.IdUsuariosNavigation).WithOne(p => p.Profesionale)
                .HasForeignKey<Profesionale>(d => d.IdUsuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profesionales_Usuarios");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Servicio__3214EC2748E93261");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Duracion)
                .HasPrecision(0)
                .HasColumnName("duracion");
            entity.Property(e => e.IdProfesional).HasColumnName("ID_Profesional");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdProfesionalNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.IdProfesional)
                .HasConstraintName("FK_Servicios_Profesionales");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Turno__3214EC2770DFA680");

            entity.ToTable("Turno");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('pendiente')")
                .HasColumnName("estado");
            entity.Property(e => e.FechaTurno)
                .HasColumnType("date")
                .HasColumnName("fechaTurno");
            entity.Property(e => e.HoraTurno)
                .HasPrecision(0)
                .HasColumnName("horaTurno");
            entity.Property(e => e.IdClientes).HasColumnName("ID_Clientes");
            entity.Property(e => e.IdProfesionales).HasColumnName("ID_Profesionales");
            entity.Property(e => e.IdServicios).HasColumnName("ID_Servicios");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("observaciones");

            entity.HasOne(d => d.IdClientesNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdClientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turno_Clientes1");

            entity.HasOne(d => d.IdProfesionalesNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdProfesionales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turno_Profesionales");

            entity.HasOne(d => d.IdServiciosNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdServicios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turno_Servicios1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC27F932E28D");

            entity.HasIndex(e => e.Username, "UQ__Username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.CreacionCuenta)
                .HasColumnType("date")
                .HasColumnName("creacionCuenta");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fechaNacimiento");
            entity.Property(e => e.FotoPerfil)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasDefaultValueSql("('https://assets.objkt.media/file/assets-003/QmZxEd5SGRHBjXJhpeGxkKoPc8thXRZ8XchavHXUq23Mbs/artifact')")
                .HasColumnName("fotoPerfil");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mail");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumTelefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numTelefono");
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pass");
            entity.Property(e => e.TipoCuenta)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("tipoCuenta");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ubicacion");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
