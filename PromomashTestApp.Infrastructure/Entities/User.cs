using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PromomashTestApp.Infrastructure.Entities;

[Table("users")]
[Index(nameof(Login), Name = "IX_users_login", IsUnique = true)]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("login")]
    public string Login { get; set; } = null!;

    [Required]
    [Column("password")]
    public string Password { get; set; } = null!;
    
    [Required]
    [Column("agreed_to_work_for_food")]
    public bool AgreedToWorkForFood { get; set; }
    
    [Required]
    [Column("country_id")]
    public Guid CountryId { get; set; }
    
    [ForeignKey(nameof(CountryId))]
    public virtual Country Country { get; set; } = null!;
    
    [Required]
    [Column("province_id")]
    public Guid ProvinceId { get; set; }
    
    [ForeignKey(nameof(ProvinceId))]
    public virtual Province Province { get; set; } = null!;
}

public class UserConfiguration : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder.Property(x => x.Id).ValueGeneratedNever();
    }
}

