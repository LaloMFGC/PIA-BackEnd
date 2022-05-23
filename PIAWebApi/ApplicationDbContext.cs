using Microsoft.EntityFrameworkCore;
using PIAWebApi.Entidades;

namespace PIAWebApi
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext( DbContextOptions options): base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RifasParticipantes>()
                .HasKey(x => new { x.RifaId, x.ParticipanteId });
        }



        public DbSet<Rifa> Rifas { get; set; }

        public DbSet<Participante> Participantes { get; set; }

        public DbSet<Ganadores> Ganadores { get; set; }

        public DbSet<RifasParticipantes> RifasParticipantes { get; set; }


    }
}
