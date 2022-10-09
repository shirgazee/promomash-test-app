using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PromomashTestApp.Infrastructure.Entities;

[Table("provinces")]
public class Province
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("name")]
    public string Name { get; set; }  = null!;
    
    [Required]
    [Column("country_id")]
    public Guid CountryId { get; set; }
    
    [ForeignKey(nameof(CountryId))]
    public virtual Country Country { get; set; } = null!;
}

public class ProvinceConfiguration : IEntityTypeConfiguration<Province> {
    public void Configure(EntityTypeBuilder<Province> builder) {
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasData(SeedData.Provinces);
    }
}
