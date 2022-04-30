using AutoMapper;
using CGARMAN.ViewModel;
using CGARMAN.ViewModel.TechnicianViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
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

    }
}
