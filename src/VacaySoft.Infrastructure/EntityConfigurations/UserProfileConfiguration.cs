using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacaySoft.Domain.Entities;

namespace VacaySoft.Infrastructure.EntityConfigurations
{
    internal class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName).HasMaxLength(250);
            builder.Property(e => e.LastName).HasMaxLength(250);
            builder.Property(e => e.MiddleName).HasMaxLength(250);
            builder.Property(e => e.UserName).HasMaxLength(100);
            builder.Property(e => e.Email).HasMaxLength(500);
            builder.Property(e => e.TemporaryEmailAddress).HasMaxLength(500);

            builder.HasIndex(e => e.Email);
        }
    }
}
