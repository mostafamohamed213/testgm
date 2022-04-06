using CGARMAN.Services;
using CGARMAN.ViewModel.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class CostCenterController : Controller
    {
        private CostCenterServices services;
        public CostCenterController(CostCenterServices _services)
        {
            services = _services;
        }
        public IActionResult Index(int CurrentPageIndex = 1, string label = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(label))
                {
                    return View(services.Search(label, 1));
                }
                return View(services.getAllCostCentersPaging(CurrentPageIndex));
            }
            catch (Exception ex)
            {

                return View("Error", ex);
            }

        }
        [HttpPost]
        public IActionResult Changelength(int length, int CurrentPageIndex = 1)
        {
            try
            {
                return View("Index", services.getAllCostCentersPagingWithChangelength(CurrentPageIndex, length));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            try
            {
                var values = services.AutoComplete(prefix);
                return Json(values);
            }
            catch (Exception)
            {
                return Json(0);
            }

        }
        [HttpPost]
        public IActionResult Search(string label)
        {
            try
            {
                var values = services.Search(label, 1);
                return PartialView("_Search", values);
            }
            catch (Exception)
            {
                return PartialView("CustomError");
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost]
        public IActionResult Create(CostCenterCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.Create(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        public IActionResult ValidateIfNameExists(string Name, string CostCenterId)
        {

            int id = !string.IsNullOrEmpty(CostCenterId) ? int.Parse(CostCenterId) : 0;
            bool IfExist = false;
            if (id > 0)
            {
                IfExist = services.SearchIfNameExists(Name, id);
            }
            else
            {
                IfExist = services.SearchIfNameExists(Name);
            }

            return Json(IfExist);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var CostCenter = services.getCostCenterById(id);
                if (CostCenter is not null)
                {
                    CostCenterEditViewModel model = new CostCenterEditViewModel()
                    {
                        CostCenterId = CostCenter.CostCenterId,
                        Name = CostCenter.Name,
                        Value=CostCenter.Value                       
                    };
                    return View(model);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        public IActionResult Edit(CostCenterEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.CostCenterId > 0)
                {
                    services.Edit(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var costCenter = services.getCostCenterById(id);
                if (costCenter is not null)
                {
                    CostCenterEditViewModel model = new CostCenterEditViewModel()
                    {
                        CostCenterId = costCenter.CostCenterId,
                        Name = costCenter.Name,
                        CreateDts = costCenter.CreateDts.ToString(),
                        Value=costCenter.Value
                    };
                    ViewBag.fromDeleteAction = true;
                    return View("View", model);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        public IActionResult Delete(CostCenterEditViewModel model)
        {
            try
            {
                if (model.CostCenterId > 0)
                {
                    services.Delete(model.CostCenterId);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpGet]
        public IActionResult View(int id)
        {
            try
            {
                var costCenter = services.getCostCenterById(id);
                if (costCenter is not null)
                {
                    CostCenterEditViewModel model = new CostCenterEditViewModel()
                    {
                        CostCenterId = costCenter.CostCenterId,
                        Name = costCenter.Name,
                        CreateDts = costCenter.CreateDts.ToString(),
                        Value= costCenter.Value
                    };
                    ViewBag.fromDeleteAction = false;
                    return View("View", model);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
    }
}
