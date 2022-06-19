using CGARMAN.Services;
using CGARMAN.ViewModel.Shared;
using CGARMAN.ViewModel.TechnicianViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Controllers
{
    public class TechnicianCompanyController : Controller
    {
        private CompanyServices services;
        public TechnicianCompanyController(CompanyServices _services)
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
                return View(services.getAllCompaniesPaging(CurrentPageIndex));
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
                return View("Index", services.getAllCompaniesPagingWithChangelength(CurrentPageIndex, length));
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
                var values = services.Search(label,1);
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
        public IActionResult Create(CreateCompanyViewModel model)
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
        public IActionResult ValidateIfNameExists(string Name ,string TechnicianCompanyId)
        {
            
            int id = !string.IsNullOrEmpty(TechnicianCompanyId) ? int.Parse(TechnicianCompanyId) : 0;
            bool IfExist = false;
            if ( id > 0 )
            {
                IfExist = services.SearchIfNameExists(Name,id);
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
               var company = services.getCompanyById(id);
                if (company is not null)
                {
                    EditCompanyViewModel model = new EditCompanyViewModel()
                    {
                        TechnicianCompanyId = company.TechnicianCompanyId,                       
                        Name = company.Name
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
        public IActionResult Edit(EditCompanyViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.TechnicianCompanyId > 0)
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
                var company = services.getCompanyById(id);
                if (company is not null)
                {
                    EditCompanyViewModel model = new EditCompanyViewModel()
                    {
                        TechnicianCompanyId = company.TechnicianCompanyId,                      
                        Name = company.Name,
                        CreateDts=company.CreateDts.ToString()
                    };
                    ViewBag.fromDeleteAction = true;
                    return View("View",model);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        public IActionResult Delete(EditCompanyViewModel model)
        {
            try
            {
                if ( model.TechnicianCompanyId > 0)
                {
                    services.Delete(model.TechnicianCompanyId);
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
                var company = services.getCompanyById(id);
                if (company is not null)
                {
                    EditCompanyViewModel model = new EditCompanyViewModel()
                    {
                        TechnicianCompanyId = company.TechnicianCompanyId,
                        Name = company.Name,
                        CreateDts = company.CreateDts.ToString()
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
