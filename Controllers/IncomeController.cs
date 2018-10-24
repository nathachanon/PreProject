using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MASdemo.Controllers
{
    public class IncomeController : Controller
    {

        public IActionResult RevenueDorm(int did)
        {
            ViewBag.did = did;
            return View();
        }

        public IActionResult Revenue()
        {

            return View();
        }


    }
}