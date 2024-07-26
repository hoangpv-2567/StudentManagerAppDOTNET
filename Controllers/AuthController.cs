using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using StudentManagerApp.Controllers;
using StudentManagerApp.Models;

public class LoginModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}

namespace StudentManagerApp.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthController : ApplicationController<AuthController>
    {
        private readonly JwtHelper _jwtHelper;

        public AuthController(ILogger<AuthController> logger, JwtHelper jwtHelper)
            : base(logger)
        {
            _jwtHelper = jwtHelper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (IsValidUser(model.Username, model.Password))
                {
                    var token = _jwtHelper.GenerateJwtToken(model.Username);
                    return Ok(new { Token = token });
                }

                return Unauthorized("Invalid login attempt.");
            }
            return BadRequest("Invalid model state.");
        }

        private bool IsValidUser(string username, string password)
        {
            return username == "testuser" && password == "password";
        }
    }
}
