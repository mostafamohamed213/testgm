using CGARMAN.ViewModel;
using CGARMAN.ViewModel.Vehicle;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.Services
{
    public class TireSizeServices
    {
        IUnitOfWork unitOfWork;
        public TireSizeServices(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            
        }
        public PagingViewModel<TireSize> getAllTireSizesPaging(int currentPage)
        {
            PagingViewModel<TireSize> model = new PagingViewModel<TireSize>();
            var tireSizes = unitOfWork.TireSizes.FindAll(c => c.Enable, (currentPage - 1) * TablesMaxRows.TireSizeIndex, TablesMaxRows.TireSizeIndex, d => d.TireSizeId, OrderBy.Ascending);
            model.items = tireSizes.ToList();
            var itemsCount = unitOfWork.TireSizes.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(TablesMaxRows.TireSizeIndex));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = TablesMaxRows.TireSizeIndex;
            return model;
        }
        public PagingViewModel<TireSize> getAllTireSizesPagingWithChangelength(int currentPageIndex, int length)
        {
            TablesMaxRows.TireSizeIndex = length;
            return getAllTireSizesPaging(currentPageIndex);
        }

        internal List<AutoCompleteViewModel> AutoComplete(string prefix)
        {
            List<AutoCompleteViewModel> values =
                unitOfWork.TireSizes.GetAllWithCriteria(c => c.Enable && c.Name.ToLower().StartsWith(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.TireSizeId, label = c.Name }).ToList();
            if (values.Count < 15)
            {
                values.AddRange(unitOfWork.TireSizes.GetAllWithCriteria(c => c.Enable && !c.Name.ToLower().StartsWith(prefix.ToLower()) && c.Name.ToLower().Contains(prefix.ToLower())).Select(c => new AutoCompleteViewModel { val = c.TireSizeId, label = c.Name }).ToList());
                if (values.Count > 15)
                {
                    values.RemoveRange(15, values.Count - 15);
                }
            }
            return values;
        }

        internal void Create(string name)
        {
            TireSize tireSize = new TireSize()
            {
                Name = name.Trim(),            
                Enable = true,
                SystemUserCreate="1",
                CreateDts=DateTime.Now
            };
            unitOfWork.TireSizes.Add(tireSize);
            unitOfWork.Complete();
        }

        internal bool SearchIfNameExists(string name, int id = 0)
        {
            return unitOfWork.TireSizes.GetOne(c => c.Name.ToLower().Trim() == name.ToLower().Trim() && c.TireSizeId != id) == null ? true : false;
        }

        internal TireSize getTireSizeById(int id)
        {
            return unitOfWork.TireSizes.GetOne(c => c.TireSizeId == id);
        }

        internal PagingViewModel<TireSize> Search(string Name, int currentPage)
        {
            var tireSizes = unitOfWork.TireSizes.GetAllWithCriteria(c => c.Enable && !string.IsNullOrEmpty(Name) ? c.Name.ToLower().Contains(Name.ToLower()) : false, null);
            PagingViewModel<TireSize> model = new PagingViewModel<TireSize>();
            model.items = tireSizes.ToList();
            var itemsCount = tireSizes.Count(c => c.Enable);
            double pageCount = (double)(itemsCount / Convert.ToDecimal(1000));
            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;
            model.itemsCount = itemsCount;
            model.Tablelength = 1000;
            return model;
        }

        internal void Edit(TireSizeEditViewModel model)
        {
            var tireSize = getTireSizeById(model.TireSizeId);
            tireSize.Name = model.Name.Trim();
            unitOfWork.Complete();
        }

        internal void Delete(int TireSizeId)
        {
            var tireSize = getTireSizeById(TireSizeId);
            tireSize.Enable = false;
            unitOfWork.Complete();
        }
    }
}
