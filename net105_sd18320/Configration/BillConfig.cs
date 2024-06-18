using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using net105_sd18320.Models;

namespace net105_sd18320.Configration
{
    public class BillConfig : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(p => p.Id);
            // khóa ngoại 
            builder.HasOne(p=>p.Account).WithMany(p => p.Bill).HasForeignKey(p => p.Username);
            //
            // builder.HasAlternateKey(); // Set thuộc tính là Unique
        }
    }
}
