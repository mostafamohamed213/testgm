using CGARMAN.ViewModel;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Services
{
    public class MaintenancesServices
    {
        IUnitOfWork unitOfWork;
        public MaintenancesServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        internal PagingViewModel<WorkOrder> getAllWorkOrdersPaging(int currentPage, long vehicleId = 0, long workOrderNumber = 0, int? workOrderStatus = null, DateTime? date = null)
        {
            PagingViewModel<WorkOrder> model = new PagingViewModel<WorkOrder>();
            var WorkOrders = unitOfWork.WorkOrder.FindAll(c => c.Enable && ( workOrderNumber > 0 ? c.WorkOrderNumber== workOrderNumber :true)  && ( workOrderStatus.HasValue && workOrderStatus.Value == 2 ? c.IsFinished: !c.IsFinished) && (vehicleId > 0 ? c.VehicleId == vehicleId : true) && (date.HasValue ? c.StartTime.Date == date.Value.Date : true), 
                (currentPage - 1) * TablesMaxRows.WorkOrderIndex, TablesMaxRows.WorkOrderIndex, d => d.WorkOrderNumber, OrderBy.Descending, new[] { "Vehicle.VehicleLicenses", "Vehicle.VehicleFamily", "Schedule"});
           
            model.items = WorkOrders.ToList();
            var itemsCount = unitOfWork.WorkOrder.Count(c => c.Enable && (workOrderNumber > 0 ? c.WorkOrderNumber == workOrderNumber : true) && (workOrderStatus.HasValue && workOrderStatus.Value == 2 ? c.IsFinished : !c.IsFinished) && (vehicleId > 0 ? c.VehicleId == vehicleId : true) && (date.HasValue ? c.StartTime.Date == date.Value.Date : true));
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.WorkOrderIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.WorkOrderIndex;
            return model;
        }
        public PagingViewModel<WorkOrder> getAllWorkOrdersPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.WorkOrderIndex = length;
            return getAllWorkOrdersPaging(currentPageIndex);
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
    }
}
