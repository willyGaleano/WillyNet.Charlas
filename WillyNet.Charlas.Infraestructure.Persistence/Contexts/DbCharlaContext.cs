using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Domain.Common;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Infraestructure.Persistence.Contexts
{
    public class DbCharlaContext : IdentityDbContext<UserApp>
    {
      
         private readonly IDateTimeService _dateTime;
         private readonly IAuthenticatedUserService _authenticatedUser;
     
        public DbCharlaContext(
            DbContextOptions options,
             IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser
            ) : base(options)
        {
           
              ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
            
        }

        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Charla> Charlas { get; set; }
        public DbSet<Control> Controls { get; set; }
        public DbSet<EstadoAsistencia> EstadosAsistencias { get; set; }
        public DbSet<EstadoEvento> EstadosEventos { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }




        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.HasKey(p => p.AsistenciaId);
                entity.ToTable("Asistencia");                
                entity.HasOne(o => o.Evento)
                     .WithMany(m => m.Asistencias)
                     .HasForeignKey(f => f.EventoId);
                entity.HasOne(o => o.EstadoAsistencia)
                     .WithMany(m => m.Asistencias)
                     .HasForeignKey(f => f.EstadoAsistenciaId);
                entity.HasOne(o => o.UserApp)
                     .WithMany(m => m.Asistencias)
                     .HasForeignKey(f => f.UserAppId);
            });

            modelBuilder.Entity<Charla>(entity =>
            {
                entity.HasKey(p => p.CharlaId);
                entity.ToTable("Charla");
                entity.Property(p => p.Nombre)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(p => p.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.Property(p => p.UrlImage)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Control>(entity =>
            {
                entity.HasKey(p => p.ControlId);
                entity.ToTable("Control");
                entity.HasOne(o => o.UserApp)
                       .WithMany(m => m.Controls)
                       .HasForeignKey(f => f.UserAppId);
            });

            modelBuilder.Entity<EstadoAsistencia>(entity =>
            {
                entity.HasKey(p => p.EstadoAsistenciaId);
                entity.ToTable("EstadoAsistencia");
                entity.Property(p => p.Nombre)
                   .IsRequired()
                   .HasMaxLength(40);
            });

            modelBuilder.Entity<EstadoEvento>(entity =>
            {
                entity.HasKey(p => p.EstadoEventoId);
                entity.ToTable("EstadoEvento");
                entity.Property(p => p.Nombre)
                   .IsRequired()
                   .HasMaxLength(40);
                entity.Property(p => p.Color)
                   .IsRequired()
                   .HasMaxLength(100);
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(p => p.EventoId);
                entity.ToTable("Evento");
                entity.HasOne(o => o.EstadoEvento)
                     .WithMany(m => m.Eventos)
                     .HasForeignKey(f => f.EstadoEventoId);
                entity.HasOne(o => o.Charla)
                     .WithMany(m => m.Eventos)
                     .HasForeignKey(f => f.CharlaId);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(p => p.RefreshTokenId);
                entity.ToTable("RefreshToken");
                entity.HasOne(o => o.UserApp)
                    .WithMany(m => m.RefreshTokens)
                    .HasForeignKey(f => f.UserAppId);
            });
        }

    }
}
