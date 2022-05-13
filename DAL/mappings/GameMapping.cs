using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL
{
    internal class GameMapping : IEntityTypeConfiguration<stats>
    {
        public void Configure(EntityTypeBuilder<stats> builder)
        {

        }
    }
}