using AutoMapper;
using CGARMAN.ViewModel;
using CGARMAN.ViewModel.Shared;
using CGARMAN.ViewModel.TechnicianViewModels;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Services
{
    public class TechniciansServices
    {
        IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public TechniciansServices(IUnitOfWork _unitOfWork, IMapper _mapper, IWebHostEnvironment hostEnvironment)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            webHostEnvironment = hostEnvironment;
        }
        public TechniciansPagingViewModel getAllTechniciansPaging(int currentPage)
        {
            //int maxRows123 = 10;
            TechniciansPagingViewModel model = new TechniciansPagingViewModel();
            var Technicians = unitOfWork.Technician.
                FindAll(c=>c.Enable, (currentPage - 1) * TablesMaxRows.TechnicianIndex, TablesMaxRows.TechnicianIndex, d => d.TechnicianId, OrderBy.Ascending, includes: new[] { "TechnicianCompany", "TechnicianPosition"});
            model.technicians = Technicians.ToList();
            var itemsCount = unitOfWork.Technician.Count(c=>c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.TechnicianIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.TechnicianIndex;
            return model;
        }

       


        internal ViewTechnicianViewModel getTechnicianDetails(int id,DateTime? datefrom = null, DateTime? dateto = null)
        {
            var tech = unitOfWork.Technician.GetOne(c => c.TechnicianId == id, new[] { "CostCenter", "TechnicianCompany", "TechnicianPosition" });
            ViewTechnicianViewModel model = new ViewTechnicianViewModel();
            model.technician = tech;
            model.maintenanceItems = unitOfWork.MaintenanceItem.FindAll(c => c.TechnicianId == tech.TechnicianId, null, 20, c => c.MaintenanceItemId, OrderBy.Descending, new[] { "MaintenanceItemType", "MaintenanceItemStatus", "MaintenanceAction", "Maintenance.Workshop" }).ToList();
            //model.maintenanceItems = unitOfWork.MaintenanceItem.GetAllWithCriteria(c => c.TechnicianId == tech.TechnicianId, new[] { "MaintenanceItemType", "MaintenanceItemStatus", "MaintenanceAction", "Maintenance.Workshop" }).ToList(); ;
            model.TechnicianAttendances = unitOfWork.TechnicianAttendance.FindAll(c => c.TechnicianId == tech.TechnicianId && (datefrom.HasValue ? c.EventDate.Date >= datefrom.Value.Date : true) && (dateto.HasValue ? c.EventDate.Date <= dateto.Value.Date : true), null, 20, c => c.CreateDts, OrderBy.Descending, new[] { "AttendanceStatus", "Shift"}).ToList();
            return model;
        }
        internal int Delete(int technicianId)
        {
          Technician technician =  unitOfWork.Technician.GetOne(c=>c.TechnicianId == technicianId);
            if (technician is not null)
            {
                technician.Enable = false;
                unitOfWork.Complete();
                return technician.TechnicianId;
            }
            return  0;
        }

        internal List<AutoCompleteViewModel> AutoCompleteTechnicianName(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.Technician.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.TechnicianId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.Technician.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.TechnicianId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }
        internal int update(int technicianId, CreateTechnicianViewModel model)
        {
            Technician technician = unitOfWork.Technician.GetOne(c => c.TechnicianId == technicianId);
            if (technician is not null)
            {
                technician.BirthDate = model.BirthDate;
                technician.Contact1 = model.Contact1;
                if (!string.IsNullOrEmpty(model.Contact2))
                {
                    technician.Contact2 = model.Contact2;
                }
                technician.CostCenterId = model.CostCenterId;
                technician.EndDate = model.EndDate;
                technician.Name = model.Name;
                technician.NationalId = model.NationalId;
                technician.StartDate = model.StartDate;
                technician.TechnicianPositionId = model.TechnicianPositionId;
                technician.TechnicianCompanyId = model.TechnicianCompanyId;
                technician.TechnicianCompanyEmployeeId = model.TechnicianCompanyEmployeeId;
                unitOfWork.Complete();
                return technicianId;
            }
            return -1;
        }

        internal int CreateTechnician(CreateTechnicianViewModel model)
        {
            try
            {
                Technician technician = mapper.Map<Technician>(model);
                technician.CreateDts = DateTime.Now;
                technician.Enable = true;
                technician.Systemusercrate = "1";
                unitOfWork.Technician.Add(technician);
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
           
        }

        internal SelectList GetAllCostCenters()
        {
            var Centers = unitOfWork.CostCenters.GetAllWithCriteria(c => c.Enable);
            if (Centers.Count() > 0)
            {
                return new SelectList(Centers, "CostCenterId", "Value");
            }
            return null;
        }

        internal SelectList GetAllTechnicianPositions(int positionsId = 0)
        {
            var positions = unitOfWork.TechnicianPosition.GetAllWithCriteria(c => c.Enable);
            if (positions.Count() > 0)
            {
                if (positionsId > 0)
                {
                    return new SelectList(positions, "TechnicianPositionId", "Name", positionsId);
                }
                return new SelectList(positions, "TechnicianPositionId", "Name");
            }
            return null;
        }

        internal SelectList GetAllCompanies(int companyId=0)
        {
            var companies = unitOfWork.TechnicianCompany.GetAllWithCriteria(c=>c.Enable);
            if (companies.Count() > 0)
            {
                if (companyId > 0)
                {
                    return new SelectList(companies, "TechnicianCompanyId", "Name",companyId);
                }
                return new SelectList(companies, "TechnicianCompanyId", "Name");
            }
            return null;
        }

        public TechniciansPagingViewModel getAllTechniciansPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.TechnicianIndex = length;
            return getAllTechniciansPaging(currentPageIndex);
        }
        internal TechniciansPagingViewModel Search(int TechnicianID, string Name, int PositionsId, int CompanyId,int currentPage)
        {
            var Technicians = unitOfWork.Technician.GetAllWithCriteria(c => c.Enable && ( c.TechnicianId == TechnicianID || (!string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false) || c.TechnicianCompanyId == CompanyId || c.TechnicianPositionId == PositionsId), new[] { "TechnicianCompany", "TechnicianPosition" });
            TechniciansPagingViewModel model = new TechniciansPagingViewModel();
            model.technicians = Technicians.ToList();
            var itemsCount = Technicians.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(5000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 5000;
            return model;        
        }
        internal ImportFileStatus GetDataFromCSVFile(IFormFile file)
        {          
            var filePath = Path.GetTempFileName();           
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reder = ExcelReaderFactory.CreateReader(stream))
            {
                try
                {
                    if (reder.RowCount <= 1)
                    {
                        return new ImportFileStatus { status = 0, message = $"This file is empty." };
                    }
                    List<Technician> TechnicianList = new List<Technician>();
                    DateTime now = DateTime.Now;
                    Dictionary<string, int> costCenters = unitOfWork.CostCenters.GetAllWithCriteria(c=>c.Enable).ToDictionary(i => i.Name, i => i.CostCenterId);
                    Dictionary<string, int> companies = unitOfWork.TechnicianCompany.GetAllWithCriteria(c => c.Enable).ToDictionary(i => i.Name, i => i.TechnicianCompanyId);
                    Dictionary<string, int> positions = unitOfWork.TechnicianPosition.GetAllWithCriteria(c => c.Enable).ToDictionary(i => i.Name, i => i.TechnicianPositionId);

                    int rowNumber = 1; 
                    while (reder.Read())
                    {
                        if (rowNumber > 1)// ignore header
                        {
                            //validation
                            if (reder.GetValue(0) == null || string.IsNullOrWhiteSpace(reder.GetValue(0).ToString())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Name." };
                            if (reder.GetValue(1) == null || string.IsNullOrWhiteSpace(reder.GetValue(1).ToString()) || !companies.ContainsKey(reder.GetValue(1).ToString().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Company." };
                            if (reder.GetValue(2) == null || string.IsNullOrWhiteSpace(reder.GetValue(2).ToString())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Employee ID." };
                            if (reder.GetValue(3) == null || string.IsNullOrWhiteSpace(reder.GetValue(3).ToString()) || !positions.ContainsKey(reder.GetValue(3).ToString().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Position." };
                            if (reder.GetValue(4) == null || string.IsNullOrWhiteSpace(reder.GetValue(4).ToString()) || !costCenters.ContainsKey(reder.GetValue(4).ToString().Trim())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Cost Center." };
                            if (reder.GetValue(5) == null || string.IsNullOrWhiteSpace(reder.GetValue(5).ToString())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # National ID." };
                            DateTime BirthDate;
                            if (reder.GetValue(6) == null || string.IsNullOrWhiteSpace(reder.GetValue(6).ToString()) || !DateTime.TryParse(reder.GetValue(6).ToString().Trim(),out BirthDate)) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Birth Date." };
                            DateTime StartDate;
                            if (reder.GetValue(7) == null || string.IsNullOrWhiteSpace(reder.GetValue(7).ToString()) || !DateTime.TryParse(reder.GetValue(7).ToString().Trim(), out StartDate)) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Start Date." };
                            DateTime? endDate =null;
                            if (reder.GetValue(8) != null && !string.IsNullOrWhiteSpace(reder.GetValue(8).ToString()))
                            {
                                DateTime End;
                                if (reder.GetValue(8) == null || string.IsNullOrWhiteSpace(reder.GetValue(8).ToString()) || !DateTime.TryParse(reder.GetValue(8).ToString().Trim(), out End)) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # End Date." };
                                endDate = End;
                            }
                            if (reder.GetValue(9) == null || string.IsNullOrWhiteSpace(reder.GetValue(9).ToString())) return new ImportFileStatus { status = 0, message = $"File format is not valid at line #{rowNumber} , column # Contact#1." };


                            TechnicianList.Add(new Technician
                            {
                                Name = reder.GetValue(0).ToString().Trim(),
                                TechnicianCompanyId = companies[reder.GetValue(1).ToString().Trim()],
                                TechnicianCompanyEmployeeId = reder.GetValue(2).ToString().Trim(),
                                TechnicianPositionId = positions[reder.GetValue(3).ToString().Trim()],
                                CostCenterId = costCenters[reder.GetValue(4).ToString().Trim()],
                                NationalId = reder.GetValue(5).ToString().Trim(),
                                BirthDate = BirthDate,
                                StartDate = StartDate,
                                EndDate = endDate.HasValue ? endDate :null,
                                Contact1 = reder.GetValue(9).ToString().Trim(),
                                Contact2 = (reder.GetValue(10) == null || string.IsNullOrWhiteSpace(reder.GetValue(10).ToString())) ? null : reder.GetValue(10).ToString().Trim(),
                                CreateDts = now,
                                Enable = true,
                                Systemusercrate = "1",                            
                            });
                       
                        }
                        rowNumber++;
                    }
               
                    unitOfWork.Technician.AddRange(TechnicianList);
                    unitOfWork.Complete();
                }
                catch (Exception ex)
                {
                    if (ex is IndexOutOfRangeException)
                    {
                        return new ImportFileStatus { status = 0, message = $"File format is not valid" };
                    }
                    return new ImportFileStatus { status = 0, message = $"Error : {ex.Message}" };
                }
            }
            return new ImportFileStatus { status = 1, message = "Successful" };
        }

        internal List<CostCenter> GetCostCentersForExportToExcel()
        {
            List<CostCenter> model = new List<CostCenter>();
            model.Add(new CostCenter { Name = "Name", Value = " Value" });
            model.AddRange(unitOfWork.CostCenters.GetAllWithCriteria(c => c.Enable).ToList());
           return model;           
        }
        internal List<TechnicianPosition> GetPositionsForExportToExcel()
        {
            List<TechnicianPosition> model = new List<TechnicianPosition>();
            model.Add(new TechnicianPosition { Name = "Name" });
            model.AddRange(unitOfWork.TechnicianPosition.GetAllWithCriteria(c => c.Enable).ToList());
            return model;
        }

        internal List<TechnicianCompany> GetCompaniesForExportToExcel()
        {
            List<TechnicianCompany> model = new List<TechnicianCompany>();
            model.Add(new TechnicianCompany { Name = "Name" });
            model.AddRange(unitOfWork.TechnicianCompany.GetAllWithCriteria(c => c.Enable).ToList());
            return model;
        }
        //internal List<Technician> GetDataFromCSVFile1(IFormFile file)
        //{
        //    List<Technician> TechnicianList = new List<Technician>();
        //    string dateFormat = "dd/MM/yyyy";
        //    int partsCount = 9;
        //    StreamReader stream = new StreamReader(file.OpenReadStream());
        //    string part;
        //    int count = 0;

        //    DateTime now = DateTime.Now;


        //    while (!stream.EndOfStream)
        //    {
        //        if (count == 0)
        //        {
        //            stream.ReadLine();
        //            count++;
        //            continue;
        //        }
        //        part = stream.ReadLine();
        //        //if (part.Length != partsCount) throw new Exception($"File format is not valid at line #{count}.");
        //        TechnicianList.Add(new Technician
        //        {
        //            //Name = part["Name"].ToString(),
        //            //TechnicianCompanyId = companies[objDataRow["Company"].ToString()],
        //            //TechnicianPositionId = positions[objDataRow["Position"].ToString()],
        //            //CostCenterId = costCenters[objDataRow["Cost Center"].ToString()],
        //            //TechnicianCompanyEmployeeId = objDataRow["Employee ID"].ToString(),
        //            //NationalId = objDataRow["National ID"].ToString(),
        //            //BirthDate = Convert.ToDateTime(objDataRow["Birth Date"].ToString()),
        //            //StartDate = Convert.ToDateTime(objDataRow["Start Date"].ToString()),
        //            //EndDate = string.IsNullOrEmpty(objDataRow["End Date"].ToString()) ? null : Convert.ToDateTime(objDataRow["End Date"].ToString()),
        //            //Contact1 = objDataRow["Contact#1"].ToString(),
        //            //Contact2 = string.IsNullOrEmpty(objDataRow["Contact#2"].ToString()) ? null : objDataRow["Contact#2"].ToString(),
        //            //CreateDts = now,
        //            //Enable = true,
        //            //Systemusercrate = "1",
        //        }); ;
        //        count++;
        //    }


        //    return TechnicianList;
        //}
    }
}
