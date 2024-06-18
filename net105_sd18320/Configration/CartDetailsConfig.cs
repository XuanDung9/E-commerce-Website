using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using net105_sd18320.Models;

namespace net105_sd18320.Configration
{
    public class CartDetailsConfig : IEntityTypeConfiguration<CartDetails>
    {
        public void Configure(EntityTypeBuilder<CartDetails> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Cart).WithMany(x => x.Details).HasForeignKey(x => x.Username).HasConstraintName("FK_Cart_CartDetails");
            
        }
    }
}
