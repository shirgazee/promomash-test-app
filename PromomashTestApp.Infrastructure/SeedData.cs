using PromomashTestApp.Infrastructure.Entities;

namespace PromomashTestApp.Infrastructure;

public class SeedData
{
    public static Country[] Countries => new[]
    {
        new Country
        {
            Id = Guid.Parse("250C8522-EE4D-42FA-BA06-97DC33F7F3AF"),
            Name = "Country 1"
        },
        new Country
        {
            Id = Guid.Parse("EF82605C-046C-403E-A7EC-F40D799F64F7"),
            Name = "Country 2"
        },
    };

    public static Province[] Provinces => new[]
    {
        new Province
        {
            Id = Guid.Parse("09D8C1CF-9F0D-4C66-9D8B-109767076015"),
            Name = "Provice 1.1",
            CountryId = Guid.Parse("250C8522-EE4D-42FA-BA06-97DC33F7F3AF")
        },
        new Province
        {
            Id = Guid.Parse("F67558E4-3103-4E2E-AD25-91F2F666EDB0"),
            Name = "Provice 1.2",
            CountryId = Guid.Parse("250C8522-EE4D-42FA-BA06-97DC33F7F3AF")
        },
        new Province
        {
            Id = Guid.Parse("A35F5EB1-6782-4F47-9EF8-F004FCBE2269"),
            Name = "Provice 2.1",
            CountryId = Guid.Parse("EF82605C-046C-403E-A7EC-F40D799F64F7")
        },
        new Province
        {
            Id = Guid.Parse("99F881E9-4207-47CB-8960-EDD2361CE212"),
            Name = "Provice 2.2",
            CountryId = Guid.Parse("EF82605C-046C-403E-A7EC-F40D799F64F7")
        }
    };
}
