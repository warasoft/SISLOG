using SISLOG.Models;
using Microsoft.EntityFrameworkCore;

namespace SISLOG.DataBase
{
    public class AccesoDbContext : DbContext
    {
        public AccesoDbContext(DbContextOptions<AccesoDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}