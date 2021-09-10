using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class BlogContext :DbContext
    {
        public DbSet<Reteta> Retete { get; set; }
        public DbSet<Utilizator> Utilizatori { get; set; }
        public DbSet<Carte> Carti { get; set; }

        public DbSet<UtilizatorLogin> UtilizatorLogins { get; set; }

        public System.Data.Entity.DbSet<WebApplication1.Models.Cos> Cos { get; set; }
    }
}