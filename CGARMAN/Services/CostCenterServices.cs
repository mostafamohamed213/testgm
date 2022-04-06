using CGARMAN.ViewModel;
using CGARMAN.ViewModel.Shared;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Services
{
    public class CostCenterServices
    {
        IUnitOfWork unitOfWork;
        
        public CostCenterServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public PagingViewModel<CostCenter> getAllCostCentersPaging(int currentPage)
        {
            PagingViewModel<CostCenter> model = new PagingViewModel<CostCenter>();
            var costCenters = unitOfWork.CostCenters.FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.CostCenterIndex,TablesMaxRows.CostCenterIndex, d => d.CostCenterId, OrderBy.Ascending);
            model.items = costCenters.ToList();
            var itemsCount = unitOfWork.CostCenters.Count(c=>c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.CostCenterIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength =TablesMaxRows.CostCenterIndex;
            return model;
        }
        public PagingViewModel<CostCenter> getAllCostCentersPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.CostCenterIndex = length;
            return getAllCostCentersPaging(currentPageIndex);
        }

        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.CostCenters.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.CostCenterId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.CostCenters.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.CostCenterId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal void Create(CostCenterCreateViewModel model)
        {
            CostCenter costCenter = new CostCenter()
            {
                Name = model.Name.Trim(),
                CreateDts = DateTime.Now,
                Enable = true,
                Value = model.Value.Trim(),
                Systemusercrate = "1"                
            };
            unitOfWork.CostCenters.Add(costCenter);
            unitOfWork.Complete();
        }

        internal bool SearchIfNameExists(string name, int id = 0)
        {
            return unitOfWork.CostCenters.GetOne(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.CostCenterId != id) == null ? true : false;
        }

        internal CostCenter getCostCenterById(int id)
        {
            return unitOfWork.CostCenters.GetOne(c => c.CostCenterId == id);
        }

        internal PagingViewModel<CostCenter> Search(string Name, int currentPage)
        {
            var CostCenters = unitOfWork.CostCenters.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false, null);
            PagingViewModel<CostCenter> model = new PagingViewModel<CostCenter>();
            model.items = CostCenters.ToList();
            var itemsCount = CostCenters.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }

        internal void Edit(CostCenterEditViewModel model)
        {
            var CostCenter = getCostCenterById(model.CostCenterId);
            CostCenter.Name = model.Name.Trim();
            CostCenter.Value = model.Value.Trim();
            unitOfWork.Complete();
        }

        internal void Delete(int CostCenterId)
        {
            var CostCenter = getCostCenterById(CostCenterId);
            CostCenter.Enable = false;
            unitOfWork.Complete();
        }
    }
}
