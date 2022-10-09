using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PromomashTestApp.Infrastructure.Entities;

[Table("countries")]
public class Country
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required] [Column("name")] 
    public string Name { get; set; } = null!;
}

public class CountryConfiguration : IEntityTypeConfiguration<Country> {
    public void Configure(EntityTypeBuilder<Country> builder) {
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasData(SeedData.Countries);
    }
}
