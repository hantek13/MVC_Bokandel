using MVC_Bokhandel.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MVC_Bokhandel.DAL
{
    public class BokhandelContext : DbContext
    {
        public BokhandelContext() : base("BokhandelContext")
        {
        }

        public DbSet<Bok> Boks { get; set; }
        public DbSet<BokForfatter> BokForfatters { get; set; }
        public DbSet<Forfatter> Forfatters { get; set; }
        public DbSet<Kunde> Kundes { get; set; }
        public DbSet<DbBruker> Brukeres { get; set; }
        public DbSet<Kurv> Kurvs { get; set; }
        public DbSet<Leveringsadresse> Leveringsadresses { get; set; }
        public DbSet<Ordre> Ordres { get; set; }
        public DbSet<Ordrelinje> Ordrelinjes { get; set; }
        public DbSet<Poststed> Poststeds { get; set; }
        public DbSet<Sjanger> Sjangers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<MVC_Bokhandel.ViewModels.HandleKurvViewModel> HandleKurvViewModels { get; set; }
    }
}