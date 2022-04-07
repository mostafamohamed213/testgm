using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class VehicleDepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
