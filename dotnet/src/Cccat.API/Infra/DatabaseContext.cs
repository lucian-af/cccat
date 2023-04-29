using Microsoft.EntityFrameworkCore;

namespace Cccat.API.Infra
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
    }
}
