using Microsoft.AspNetCore.Mvc;
using PromomashTestApp.Controllers.Models;
using PromomashTestApp.Infrastructure.Repositories;

namespace PromomashTestApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryRepository _countryRepository;
    private readonly IProvinceRepository _provinceRepository;

    public CountryController(ICountryRepository countryRepository, IProvinceRepository provinceRepository)
    {
        _countryRepository = countryRepository;
        _provinceRepository = provinceRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCountries(CancellationToken ct)
    {
        var countries = await _countryRepository.GetCountriesAsync(ct);
        return Ok(countries.Select(x => new CountryApiModel
        {
            Id = x.Id,
            Name = x.Name
        }));
    }

    [HttpGet("{countryId:guid}/provinces")]
    public async Task<IActionResult> GetProvinces(Guid countryId, CancellationToken ct)
    {
        var provincesAsync = await _provinceRepository.GetProvincesAsync(countryId, ct);
        return Ok(provincesAsync.Select(x => new ProvinceApiModel
        {
            Id = x.Id,
            Name = x.Name
        }));
    }
}
