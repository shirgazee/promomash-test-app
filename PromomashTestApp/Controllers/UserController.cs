using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PromomashTestApp.Controllers.Models;
using PromomashTestApp.Infrastructure.Entities;
using PromomashTestApp.Infrastructure.Repositories;

namespace PromomashTestApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IValidator<UserRegisterModel> _validator;
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public UserController(
        IValidator<UserRegisterModel> validator,
        ILogger<UserController> logger,
        IUserRepository userRepository)
    {
        _validator = validator;
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterModel registerModel, CancellationToken ct)
    {
        var result = await _validator.ValidateAsync(registerModel, ct);
        if (!result.IsValid)
        {
            return BadRequest(result);
        }

        _logger.Log(LogLevel.Information, "Got new form: {Model}", JsonSerializer.Serialize(registerModel));

        var userExists = await _userRepository.AnyAsync(registerModel.Login, ct);
        if (userExists)
            return Conflict("User already registered");

        await _userRepository.AddAsync(new User
        {
            Login = registerModel.Login,
            Password = registerModel.Password,
            AgreedToWorkForFood = registerModel.AgreedToWorkForFood,
            CountryId = registerModel.CountryId,
            ProvinceId = registerModel.ProvinceId
        }, ct);

        return Ok();
    }
}
