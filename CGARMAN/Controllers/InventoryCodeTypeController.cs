using CGARMAN.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class InventoryCodeTypeController : Controller
    {
        private InventoryServices inventoryServices;
        public InventoryCodeTypeController(InventoryServices _inventoryServices)
        {
            inventoryServices = _inventoryServices;
        }
        [HttpPost]
        public IActionResult Create(string CodeTypeName)
        {
            try
            {
                if (!string.IsNullOrEmpty(CodeTypeName.Trim()))
                {
                    var status = inventoryServices.AddCodeType(CodeTypeName.Trim());
                    if (status == 1)
                    {
                        var codeTypes = inventoryServices.GetcodeTypes();
                        return Json(new { status = 1, @object = codeTypes });
                    }
                    if (status == -1)
                    {
                        return Json(new { status = -1, @object = "" });
                    }
                   
                }
                return Json(new { status = 0, @object = "" });
            }
            catch (Exception)
            {

                return Json(new { status = 0, @object = "" });
            }
           
        }
    }
}
