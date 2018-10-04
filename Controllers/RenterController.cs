using System.Linq;
using MASdemo.Context;
using MASdemo.Models;
using MASdemo.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MASdemo.Controllers
{
    public class RenterController : Controller
    {
        [HttpGet]
        public IActionResult ShowRoom()
        {
            return View();
        }

    }
}