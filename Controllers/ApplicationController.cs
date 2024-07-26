using Microsoft.AspNetCore.Mvc;
using StudentManagerApp.Models;

namespace StudentManagerApp.Controllers

{
    public abstract class ApplicationController<T> : Controller where T : Controller
    {
        protected readonly ILogger<T> _logger;

        public ApplicationController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
