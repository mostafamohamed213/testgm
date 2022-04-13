using CGARMAN.ViewModel;
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
    public class VehicleServices
    {
        IUnitOfWork unitOfWork;
        public VehicleServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public PagingViewModel<Vehicle> getAllVehiclesPaging(int currentPage , string plateNumber = null , int departmentID = 0, int ownerID =0 , int statusID =0, int familyID =0, int brandID=0)
        {
            PagingViewModel<Vehicle> model = new PagingViewModel<Vehicle>();
            var vehicles = unitOfWork.Vehicle.FindAll(c => c.Enable && (!string.IsNullOrEmpty(plateNumber) ? c.LicenseNumber.ToLower().Contains(plateNumber.ToLower()) : true)
            &&( departmentID > 0 ? departmentID == c.VehicleDepartmentId : true)
            &&( ownerID > 0 ? ownerID == c.VehicleOwnerId : true )
            &&( statusID > 0 ? statusID == c.VehicleStatusId : true)
            &&( familyID > 0 ? familyID == c.VehicleFamilyId : true)
            &&( brandID > 0 ? brandID == c.VehicleBrandId : true )
            , (currentPage - 1) * TablesMaxRows.VehicleIndex, TablesMaxRows.VehicleIndex, d => d.CreateDts, OrderBy.Descending, new[] { "VehicleFamily", "VehicleBrand", "VehicleOwner" , "VehicleStatus", "VehicleDepartment"});           
            model.items = vehicles.ToList();
            var itemsCount = unitOfWork.Vehicle.Count(c => c.Enable && !string.IsNullOrEmpty(plateNumber) ? c.LicenseNumber.ToLower().Contains(plateNumber.ToLower()) : true
            && departmentID > 0 ? departmentID == c.VehicleDepartmentId : true
            && ownerID > 0 ? ownerID == c.VehicleOwnerId : true
            && statusID > 0 ? statusID == c.VehicleStatusId : true
            && familyID > 0 ? familyID == c.VehicleFamilyId : true
            && brandID > 0 ? brandID == c.VehicleBrandId : true
           );
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.VehicleIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.VehicleIndex;
            return model;
        }
        public PagingViewModel<Vehicle> getAllVehiclesPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.VehicleIndex = length;
            return getAllVehiclesPaging(currentPageIndex);
        }
        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && c.LicenseNumber.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleId, label = c.LicenseNumber }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && !c.LicenseNumber.ToLower().StartsWith(prefix.ToLower()) && c.LicenseNumber.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.VehicleId, label = c.LicenseNumber }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal PagingViewModel<Vehicle> AdvancedSearch( string plateNumber, int departmentID, int ownerID, int statusID, int familyID, int brandID)
        {
            var Vehicles = unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable
            && !string.IsNullOrEmpty(plateNumber) ? c.LicenseNumber.ToLower().Contains(plateNumber.ToLower()) : true 
            && departmentID > 0 ?departmentID ==c.VehicleDepartmentId : true
            && ownerID > 0 ? ownerID == c.VehicleOwnerId : true
            && statusID > 0 ? statusID == c.VehicleStatusId : true
            && familyID > 0 ? familyID == c.VehicleFamilyId : true
            && brandID > 0 ? brandID == c.VehicleBrandId : true
            , new[] { "VehicleFamily", "VehicleBrand", "VehicleOwner", "VehicleStatus", "VehicleDepartment" });
            PagingViewModel<Vehicle> model = new PagingViewModel<Vehicle>();
            model.items = Vehicles.ToList();
            var itemsCount = Vehicles.Count(c => c.Enable
            && !string.IsNullOrEmpty(plateNumber) ? c.LicenseNumber.ToLower().Contains(plateNumber.ToLower()) : true
            && departmentID > 0 ? departmentID == c.VehicleDepartmentId : true
            && ownerID > 0 ? ownerID == c.VehicleOwnerId : true
            && statusID > 0 ? statusID == c.VehicleStatusId : true
            && familyID > 0 ? familyID == c.VehicleFamilyId : true
            && brandID > 0 ? brandID == c.VehicleBrandId : true
            );
            double pageCount = (double)(itemsCount / Convert.ToDecimal(100));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = 1;
            model.itemsCount = itemsCount;
            model.Tablelength = 100;
            return model;
        }

        internal SelectList GetFamilies(int FamilyId = 0)
        {
            var values = unitOfWork.VehicleFamily.GetAllWithCriteria(c => c.Enable);
            if (values.Count() > 0)
            {
                if (FamilyId > 0)
                {
                    return new SelectList(values, "VehicleFamilyId", "Name", FamilyId);
                }
                return new SelectList(values, "VehicleFamilyId", "Name");
            }
            return null;
        }

        internal SelectList GetDepartments(int DepartmentId =0)
        {
            var values = unitOfWork.VehicleDepartment.GetAllWithCriteria(c => c.Enable);
            if (values.Count() > 0)
            {
                if (DepartmentId > 0)
                {
                    return new SelectList(values, "VehicleDepartmentId", "Name", DepartmentId);
                }
                return new SelectList(values, "VehicleDepartmentId", "Name");
            }
            return null;
        }

        internal SelectList GetBrandsByfamilyId(int FamilyId, int BrandId = 0)
        {
            var values = unitOfWork.VehicleBrand.GetAllWithCriteria(c => c.Enable && c.VehicleFamilyId == FamilyId);
            if (values.Count() > 0)
            {
                if (BrandId > 0)
                {
                    return new SelectList(values, "VehicleBrandId", "Name", BrandId);
                }
                return new SelectList(values, "VehicleBrandId", "Name");
            }
            return null;
        }
        internal SelectList GetStatus(int StatusId = 0)
        {
            var values = unitOfWork.VehicleStatus.GetAllWithCriteria(c => c.Enable);
            if (values.Count() > 0)
            {
                if (StatusId > 0)
                {
                    return new SelectList(values, "VehicleStatusId", "Name", StatusId);
                }
                return new SelectList(values, "VehicleStatusId", "Name");
            }
            return null;
        }
        internal SelectList GetOwners(int OwnerId = 0)
        {
            var values = unitOfWork.VehicleOwner.GetAllWithCriteria(c => c.Enable);
            if (values.Count() > 0)
            {
                if (OwnerId > 0)
                {
                    return new SelectList(values, "VehicleOwnerId", "Name", OwnerId);
                }
                return new SelectList(values, "VehicleOwnerId", "Name");
            }
            return null;
        }
        internal PagingViewModel<Vehicle> Search(string Name, int currentPage)
        {
            var Vehicles = unitOfWork.Vehicle.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.LicenseNumber.ToLower().Contains(Name.ToLower()) : false, new[] { "VehicleFamily", "VehicleBrand", "VehicleOwner", "VehicleStatus", "VehicleDepartment" });
            PagingViewModel<Vehicle> model = new PagingViewModel<Vehicle>();
            model.items = Vehicles.ToList();
            var itemsCount = Vehicles.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }
    }
}
