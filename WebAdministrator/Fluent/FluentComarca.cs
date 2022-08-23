using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAdministrator.Models;

namespace WebAdministrator.Fluent
{
    public class FluentComarca : IEntityTypeConfiguration<Comarca>
    {
        public void Configure(EntityTypeBuilder<Comarca> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
