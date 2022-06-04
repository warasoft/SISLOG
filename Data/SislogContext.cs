using SISLOG.Models;
using Microsoft.EntityFrameworkCore;

namespace SISLOG.Data
{
    public class SislogContext : DbContext
    {
        public SislogContext(DbContextOptions<SislogContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}