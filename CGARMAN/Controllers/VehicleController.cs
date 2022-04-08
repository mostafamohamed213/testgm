using CGARMAN.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class VehicleController : Controller
    {
        private VehicleServices services;
        public VehicleController(VehicleServices _services)
        {
            services = _services;
        }
    }
}
