using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.InventoryViewModels
{
    public class InventoryItemIndexPagingViewModel
    {
        public List<InventoryItemIndexViewModel> items { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
        public int itemsCount { get; set; }
        public int Tablelength { get; set; }
    }
    public class InventoryItemTypeIndexPagingViewModel
    {
        public List<InventoryItemTypeIndexViewModel> itemTypes { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
        public int itemsCount { get; set; }
        public int Tablelength { get; set; }

    }
}
