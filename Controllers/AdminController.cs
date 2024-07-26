using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagerApp.Controllers;
using StudentManagerApp.Models;


namespace StudentManagerApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/")]
    public class AdminController : ApplicationController<AdminController>
    {
        public AdminController(ILogger<AdminController> logger)
          : base(logger)
        {
        }

        [HttpGet("profile")]
        public IActionResult Get()
        {
          var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          var username = User.Identity?.Name;

          var userInfo = new
          {
            UserId = userId,
            Username = username
          };

          return Ok(userInfo);
        }
    }
}
