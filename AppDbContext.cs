using apiCargueClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace apiCargueClientes
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Cliente> cliente { get; set; }
    }
}
