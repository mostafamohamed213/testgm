using AutoMapper;
using CGARMAN.Services;
using CGARMAN.ViewModel.Shared;
using CGARMAN.ViewModel.TechnicianViewModels;
using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace CGARMAN.Controllers
{
    public class TechniciansController : Controller
    {
        private TechniciansServices TechniciansServices;
        private readonly IMapper mapper;
        public TechniciansController(TechniciansServices _techniciansServices, IMapper _mapper)
        {
            TechniciansServices = _techniciansServices;
            mapper = _mapper;
        }
        public IActionResult Index(int CurrentPageIndex =1)
        {
            try
            {
                ViewBag.Companies = TechniciansServices.GetAllCompanies();
                ViewBag.Positions = TechniciansServices.GetAllTechnicianPositions();
                return View("Index1",TechniciansServices.getAllTechniciansPaging(CurrentPageIndex));
            }
            catch (Exception ex)
            {
                return View("Error" ,ex);
            }
         
        }
        [HttpPost]
        public IActionResult Changelength(int length, int CurrentPageIndex = 1)
        {
            try
            {               
                return View("Index1", TechniciansServices.getAllTechniciansPagingWithChangelength(CurrentPageIndex, length));
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost]
        public IActionResult Search(int technicianID, string Name, int PositionsId ,int CompanyId)
        {
            try
            {
                var values = TechniciansServices.Search(technicianID, Name, PositionsId, CompanyId,1);
                return PartialView("_Search", values);
            }
            catch (Exception)
            {
                return PartialView("CustomError");
            }
        }
        [HttpGet]
        public IActionResult ViewTechnician(int id, DateTime? datefrom = null, DateTime? dateto = null,int? isattend=null)
        {
            try
            {
                ViewBag.datefrom = datefrom;
                ViewBag.dateto = dateto;
                ViewBag.id = id;
                ViewBag.isattend = isattend;
                if (datefrom != null)
                {
                    DateTime datef = (DateTime)datefrom;
                    string ss = datef.ToString("yyyy/MM/dd");
                    ss = ss.Replace("/", "-");
                    ViewBag.datefrom = ss;
                }
                else
                {
                    ViewBag.datefrom = datefrom;
                }
                if (dateto != null)
                {
                    DateTime datet = (DateTime)dateto;
                    string s = datet.ToString("yyyy/MM/dd");
                    s = s.Replace("/", "-");

                    ViewBag.dateto = s;
                }
                else
                {
                    ViewBag.dateto = dateto;
                }

                if (datefrom != null || dateto != null)
                {
                    return View(TechniciansServices.getTechnicianDetails(id, datefrom, dateto));
                }

                var technician = TechniciansServices.getTechnicianDetails(id);
                return View(technician);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                ViewBag.TechnicianCompanies = TechniciansServices.GetAllCompanies();
                ViewBag.TechnicianPositions = TechniciansServices.GetAllTechnicianPositions();
                ViewBag.CostCenters = TechniciansServices.GetAllCostCenters();
                return View("CreateTechnician");
            }
            catch (Exception ex)
            {
                return View("Error",ex);
            }
        }
        [HttpPost]
        public IActionResult Create(CreateTechnicianViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   int status= TechniciansServices.CreateTechnician(model);
                    if (status==1)
                    {
                        return RedirectToAction("Index",new { CurrentPageIndex=1 });
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                ViewBag.TechnicianCompanies = TechniciansServices.GetAllCompanies();
                ViewBag.TechnicianPositions = TechniciansServices.GetAllTechnicianPositions();
                ViewBag.CostCenters = TechniciansServices.GetAllCostCenters();
                return View("CreateTechnician", model);
            }
            catch (Exception ex)
            {
                return View("Error",ex);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var technician = TechniciansServices.getTechnicianDetails(id);
                CreateTechnicianViewModel model = mapper.Map<CreateTechnicianViewModel>(technician.technician);
                ViewBag.TechnicianCompanies = TechniciansServices.GetAllCompanies();
                ViewBag.TechnicianPositions = TechniciansServices.GetAllTechnicianPositions();
                ViewBag.CostCenters = TechniciansServices.GetAllCostCenters();
                return View("Edit", model);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        [HttpPost]
        public IActionResult Edit(CreateTechnicianViewModel Technician)
        {
            try
            {
                if (ModelState.IsValid && Technician.TechnicianId.HasValue)
                {
                    int techId= TechniciansServices.update(Technician.TechnicianId.Value,Technician);
                    if (techId >0)
                    {
                        return RedirectToAction("ViewTechnician", new { id = techId });
                    }
                    else
                    {
                        return View("Error");
                    }                    
                }

               
                ViewBag.TechnicianCompanies = TechniciansServices.GetAllCompanies();
                ViewBag.TechnicianPositions = TechniciansServices.GetAllTechnicianPositions();
                ViewBag.CostCenters = TechniciansServices.GetAllCostCenters();
                return View("Edit", Technician);
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
                var technician = TechniciansServices.getTechnicianDetails(id);
                return View("Delete", technician);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        public IActionResult DeleteTechnician(int idTechnician)
        {
            try
            {
                var technician = TechniciansServices.Delete(idTechnician);
                if (technician > 0)
                {
                    return RedirectToAction("Index");
                }
                return View("Error");
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
                var values = TechniciansServices.AutoCompleteTechnicianName(prefix);
                return Json(values);
            }
            catch (Exception)
            {
                return Json(0);
            }
        }
        [HttpPost]
        public ActionResult ImportFile(IFormFile file)
        {
            if (file == null) return Json(new ImportFileStatus { status = 0, message = "No File Selected" });
            string ext = Path.GetExtension(file.FileName).Substring(1).ToUpper();
            if (ext == "XLSX" || ext == "XLTX" || ext == "XLTM" || ext == "XLSM" || ext == "XLS")
            {
               return Json(TechniciansServices.GetDataFromCSVFile(file));
            }
            return Json(new ImportFileStatus { status = 0, message = "Invalid File. Please upload a File withextension: XLSX or XLTX or XLTM or XLSM or XLS" });
        }

        public ActionResult ExportCostCenters()
        {
            var costCenters = TechniciansServices.GetCostCentersForExportToExcel();           
            XLWorkbook excel = new XLWorkbook();          
            excel.Worksheets.Add("CostCenters").Cell(1, 1).SetValue(costCenters.Select(c=> new { c.Name ,c.Value}));
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            MemoryStream memoryStream = new MemoryStream();
            excel.SaveAs((Stream)memoryStream);
            memoryStream.Seek(0L, SeekOrigin.Begin);
            var content = memoryStream.ToArray();
            return File(content, contentType, "CostCenters.xlsx");   
        }
        public ActionResult ExportPositions()
        {
            var Positions = TechniciansServices.GetPositionsForExportToExcel();
            XLWorkbook excel = new XLWorkbook();
            excel.Worksheets.Add("Positions").Cell(1, 1).SetValue(Positions.Select(c => new { c.Name }));
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            MemoryStream memoryStream = new MemoryStream();
            excel.SaveAs((Stream)memoryStream);
            memoryStream.Seek(0L, SeekOrigin.Begin);
            var content = memoryStream.ToArray();
            return File(content, contentType, "Positions.xlsx");
        }
        public ActionResult ExportCompanies()
        {
            var Positions = TechniciansServices.GetCompaniesForExportToExcel();
            XLWorkbook excel = new XLWorkbook();
            excel.Worksheets.Add("Companies").Cell(1, 1).SetValue(Positions.Select(c => new { c.Name }));
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            MemoryStream memoryStream = new MemoryStream();
            excel.SaveAs((Stream)memoryStream);
            memoryStream.Seek(0L, SeekOrigin.Begin);
            var content = memoryStream.ToArray();
            return File(content, contentType, "Companies.xlsx");
        }

    }
}
