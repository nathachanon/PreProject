using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MASdemo.Controllers
{
    public class StatusCodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ILogger<ManageController> _manage;
        public StatusCodeController(ILogger<ManageController> manage)
        {
            _manage = manage;
        }

        // GET: /<controller>/
        [HttpGet("/StatusCode/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            _manage.LogInformation($"Unexpected Status Code: {statusCode}, OriginalPath: {reExecute.OriginalPath}");
            return View(statusCode);
        }
    }
}