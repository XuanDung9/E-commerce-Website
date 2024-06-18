using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace net105_sd18320.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions options) : base(options) { }
        // khai báo DbSet 
        public DbSet<Account> Account { get; set; } // mỗi DbSet sẽ đại diện cho 1 bảng trong CSDL
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillDetails> BillDetails { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<Product> Product { get; set; }


        // thực hiện override các phương thức cấu hình 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-I6GJF6D7\\MSSQLSERVER03;Initial Catalog=DEMO_NET105;Integrated Security=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
