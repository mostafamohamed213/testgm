using CGARMAN.Services;
using CGARMAN.ViewModel.Shared;
using CGARMAN.ViewModel.Vehicle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class TireSizeController : Controller
    {
        private TireSizeServices services;
        public TireSizeController(TireSizeServices _services)
        {
            services = _services;
        }
        public IActionResult Index(int CurrentPageIndex = 1, string label = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(label))
                {
                    ViewBag.label = label;
                    return View(services.Search(label, 1));
                }
                return View(services.getAllTireSizesPaging(CurrentPageIndex));
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
                return View("Index", services.getAllTireSizesPagingWithChangelength(CurrentPageIndex, length));
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
        public IActionResult Create(TireSizeCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.Create(model.Name);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        public IActionResult ValidateIfNameExists(string Name, string TireSizeId)
        {

            int id = !string.IsNullOrEmpty(TireSizeId) ? int.Parse(TireSizeId) : 0;
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
                var tireSize = services.getTireSizeById(id);
                if (tireSize is not null)
                {
                    TireSizeEditViewModel model = new TireSizeEditViewModel()
                    {
                        TireSizeId = tireSize.TireSizeId,
                        Name = tireSize.Name,
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
        public IActionResult Edit(TireSizeEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.TireSizeId > 0)
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
                var tireSize = services.getTireSizeById(id);
                if (tireSize is not null)
                {
                    TireSizeEditViewModel model = new TireSizeEditViewModel()
                    {
                        TireSizeId = tireSize.TireSizeId,
                        Name = tireSize.Name,
                        CreateDts = tireSize.CreateDts.ToString()
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
        public IActionResult Delete(TireSizeEditViewModel model)
        {
            try
            {
                if (model.TireSizeId > 0)
                {
                    services.Delete(model.TireSizeId);
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
                var tireSize = services.getTireSizeById(id);
                if (tireSize is not null)
                {
                    TireSizeEditViewModel model = new TireSizeEditViewModel()
                    {
                        TireSizeId = tireSize.TireSizeId,
                        Name = tireSize.Name,
                        CreateDts = tireSize.CreateDts.ToString()
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
        [HttpPost]
        public ActionResult ImportFile(IFormFile file)
        {
            if (file == null) return Json(new ImportFileStatus { status = 0, message = "No File Selected" });
            string ext = Path.GetExtension(file.FileName).Substring(1).ToUpper();
            if (ext == "XLSX" || ext == "XLTX" || ext == "XLTM" || ext == "XLSM" || ext == "XLS")
            {
                return Json(services.GetDataFromCSVFile(file));
            }
            return Json(new ImportFileStatus { status = 0, message = "Invalid File. Please upload a File withextension: XLSX or XLTX or XLTM or XLSM or XLS" });
        }
    }
}
