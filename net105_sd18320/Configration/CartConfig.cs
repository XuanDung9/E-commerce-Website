using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using net105_sd18320.Models;

namespace net105_sd18320.Configration
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(p => p.Username);
            builder.HasOne(p => p.Account).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(p => p.Username); // Lấy toán tử xác định .HasForeignKey<Cart>, lấy thằng username của cart để nối với thằng Account
        }
    }
}
