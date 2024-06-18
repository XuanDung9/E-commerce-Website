using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using net105_sd18320.Models;

namespace net105_sd18320.Configration
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        // fluentApi được sử dụng để cấu hình các bảng dựa trên các model và được gọi là đè lên các ràng buộc có sẵn hoặc DA
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // khóa chính 
            builder.HasKey(p => p.Username);

            // key có nhiều cột
            // builder.Property(p=> new (p.Username ; p.Password); key có 2 cột 

            // builder.HasNoKey(); // không có khóa 
            // cấu hình thuộc tính 
            builder.Property(p => p.Password).HasColumnType("varchar(256)");
            builder.Property(p => p.Address).IsUnicode(true).IsFixedLength(true).HasMaxLength(256);
            // .IsUnicode(true).IsFixedLength(true).HasMaxLength(256) tương đương với nvarchar(256)
        }
    }
}
