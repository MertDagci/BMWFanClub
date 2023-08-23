using BMWFanClub.Data.MySQL;
using Microsoft.EntityFrameworkCore;

namespace BMWFanClub.Data.MySQL
{

    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        public virtual DbSet<User> Users { get; set; }
    }
}
