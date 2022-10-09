using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PromomashTestApp.Controllers.Models;

namespace PromomashTestApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IValidator<UserRegisterModel> _validator;
    private readonly ILogger<UserController> _logger;

    /// <summary>
    /// ctor
    /// </summary>
    public UserController(IValidator<UserRegisterModel> validator, ILogger<UserController> logger)
    {
        _validator = validator;
        _logger = logger;
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

        return Ok();
    }
}
