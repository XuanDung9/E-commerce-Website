using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using net105_sd18320.Models;

namespace net105_sd18320.Configration
{
    public class BillDetailsConfig : IEntityTypeConfiguration<BillDetails>
    {
        public void Configure(EntityTypeBuilder<BillDetails> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(P => P.Bill).WithMany(P => P.BillDetails).HasForeignKey(P => P.BillId) ;
            builder.HasOne(p => p.Product).WithMany(p => p.BillDetails).HasForeignKey(P => P.ProductId);

        }
    }
}
    