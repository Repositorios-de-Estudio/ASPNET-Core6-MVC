using IngresosGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace IngresosGastos.Data
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {

        }

        // Para inyeccion de dependencias
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ConexionBD");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CategoriaTipo> CategoriaTipo { get; }


    }
}
