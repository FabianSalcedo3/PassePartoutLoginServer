using Microsoft.EntityFrameworkCore;
using PassePartoutApi.Models;

namespace PassePartoutApi.data_access
{
    public class UtenteDbContext:DbContext
    {
        public UtenteDbContext(DbContextOptions<UtenteDbContext>options):base(options)
        { }

        public DbSet<Utente> Utente { get; set; } //includo il modello Utente per la migrazione
    }
}
