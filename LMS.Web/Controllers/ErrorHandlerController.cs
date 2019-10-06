using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Controllers
{
    public class ErrorHandlerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PageNotFound()
        {
            return View();
        }
    }
}