using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;


namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context
{
    public class ContextFacultadDerecho : DbContext
    {
        public ContextFacultadDerecho(DbContextOptions options) : base(options) { }

        // === DbSets para entidades ===
        public DbSet<Calendarios> Calendarios { get; set; }
        public DbSet<ConfiguracionDias> ConfiguracionDias { get; set; }
        public DbSet<Consultorios> Consultorios { get; set; }
        public DbSet<TiposDocumento> TiposDocumento { get; set; }
        public DbSet<UsuarioConsultorio> UsuarioConsultorio { get; set; }
        public DbSet<Turnos> Turnos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<LimitesTurnosConsultorio> LimitesTurnosConsultorio { get; set; }

        // 🔹 EF Core llamará aquí y nosotros invocamos tu configuración
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfuguration(modelBuilder);
        }

        // Configuración de entidades
        private void EntityConfuguration(ModelBuilder modelBuilder)
        {
            // === Calendarios ===
            modelBuilder.Entity<Calendarios>().ToTable("Calendarios");
            modelBuilder.Entity<Calendarios>().HasKey(u => u.Id);
            modelBuilder.Entity<Calendarios>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Calendarios>().Property(u => u.Anio).HasColumnName("Anio");
            modelBuilder.Entity<Calendarios>().Property(u => u.Semestre).HasColumnName("Semestre");
            modelBuilder.Entity<Calendarios>().Property(u => u.FechaInicio).HasColumnName("FechaInicio");
            modelBuilder.Entity<Calendarios>().Property(u => u.FechaFin).HasColumnName("FechaFin");
            modelBuilder.Entity<Calendarios>().Property(u => u.DiaConciliacion).HasColumnName("DiaConciliacion");
            modelBuilder.Entity<Calendarios>().Property(u => u.Estado).HasColumnName("Estado");

            // === ConfiguracionDias ===
            modelBuilder.Entity<ConfiguracionDias>().ToTable("ConfiguracionDias");
            modelBuilder.Entity<ConfiguracionDias>().HasKey(u => u.Id);
            modelBuilder.Entity<ConfiguracionDias>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<ConfiguracionDias>().Property(u => u.DiaSemana).HasColumnName("DiaSemana");
            modelBuilder.Entity<ConfiguracionDias>().Property(u => u.MaxTurnosAM).HasColumnName("MaxTurnosAM");
            modelBuilder.Entity<ConfiguracionDias>().Property(u => u.MaxTurnosPM).HasColumnName("MaxTurnosPM");
            modelBuilder.Entity<ConfiguracionDias>().Property(u => u.CalendarioId).HasColumnName("CalendarioId");

            // === Consultorios ===
            modelBuilder.Entity<Consultorios>().ToTable("Consultorios");
            modelBuilder.Entity<Consultorios>().HasKey(u => u.Id);
            modelBuilder.Entity<Consultorios>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Consultorios>().Property(u => u.Nombre).HasColumnName("Nombre");

            // === TiposDocumento ===
            modelBuilder.Entity<TiposDocumento>().ToTable("TiposDocumento");
            modelBuilder.Entity<TiposDocumento>().HasKey(u => u.Id);
            modelBuilder.Entity<TiposDocumento>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<TiposDocumento>().Property(u => u.Nombre).HasColumnName("Nombre");

            // === UsuarioConsultorio ===
            modelBuilder.Entity<UsuarioConsultorio>().ToTable("UsuarioConsultorio");
            modelBuilder.Entity<UsuarioConsultorio>().HasKey(u => u.Id);
            modelBuilder.Entity<UsuarioConsultorio>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<UsuarioConsultorio>().Property(u => u.UsuarioId).HasColumnName("UsuarioId");
            modelBuilder.Entity<UsuarioConsultorio>().Property(u => u.ConsultorioId).HasColumnName("ConsultorioId");

            // === Turnos ===
            modelBuilder.Entity<Turnos>().ToTable("Turnos");
            modelBuilder.Entity<Turnos>().HasKey(u => u.Id);
            modelBuilder.Entity<Turnos>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Turnos>().Property(u => u.UsuarioId).HasColumnName("UsuarioId");
            modelBuilder.Entity<Turnos>().Property(u => u.ConsultorioId).HasColumnName("ConsultorioId");
            modelBuilder.Entity<Turnos>().Property(u => u.Fecha).HasColumnName("Fecha");
            modelBuilder.Entity<Turnos>().Property(u => u.Jornada).HasColumnName("Jornada");
            modelBuilder.Entity<Turnos>().Property(u => u.CalendarioId).HasColumnName("CalendarioId");

            // === Rol ===
            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<Rol>().HasKey(u => u.Id);
            modelBuilder.Entity<Rol>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Rol>().Property(u => u.Nombre).HasColumnName("Nombre");

            // === Usuarios ===
            modelBuilder.Entity<Usuarios>().ToTable("Usuarios");
            modelBuilder.Entity<Usuarios>().HasKey(u => u.Id);
            modelBuilder.Entity<Usuarios>().Property(u => u.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Usuarios>().Property(u => u.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Usuarios>().Property(u => u.Documento).HasColumnName("Documento");
            modelBuilder.Entity<Usuarios>().Property(u => u.TipoDocumentoId).HasColumnName("TipoDocumentoId");
            modelBuilder.Entity<Usuarios>().Property(u => u.Correo).HasColumnName("Correo");
            modelBuilder.Entity<Usuarios>().Property(u => u.Contrasena).HasColumnName("Contrasena");
            modelBuilder.Entity<Usuarios>().Property(u => u.RolId).HasColumnName("RolId");


            // === LimitesTurnosConsultorio ===
            modelBuilder.Entity<LimitesTurnosConsultorio>().ToTable("LimitesTurnosConsultorio");
            modelBuilder.Entity<LimitesTurnosConsultorio>().HasKey(l => new { l.CalendarioId, l.ConsultorioId });
            modelBuilder.Entity<LimitesTurnosConsultorio>().Property(u => u.CalendarioId).HasColumnName("CalendarioId");
            modelBuilder.Entity<LimitesTurnosConsultorio>().Property(u => u.ConsultorioId).HasColumnName("ConsultorioId");
            modelBuilder.Entity<LimitesTurnosConsultorio>().Property(u => u.LimiteTurnos).HasColumnName("LimiteTurnos");
        }

        public async Task<bool> SaveAsync()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
