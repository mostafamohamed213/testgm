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
    public class VehicleServices
    {
        IUnitOfWork unitOfWork;
        public VehicleServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public PagingViewModel<CostCenter> getAllCostCentersPaging(int currentPage)
        {
            PagingViewModel<CostCenter> model = new PagingViewModel<CostCenter>();
            var costCenters = unitOfWork.CostCenters.FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.CostCenterIndex, TablesMaxRows.CostCenterIndex, d => d.CostCenterId, OrderBy.Ascending);
            model.items = costCenters.ToList();
            var itemsCount = unitOfWork.CostCenters.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.CostCenterIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.CostCenterIndex;
            return model;
        }
    }
}
