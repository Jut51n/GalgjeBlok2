using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL
{
    internal class SpelerMapping : IEntityTypeConfiguration<Speler>
    {
        public void Configure(EntityTypeBuilder<Speler> builder)
        {

        }
    }
}