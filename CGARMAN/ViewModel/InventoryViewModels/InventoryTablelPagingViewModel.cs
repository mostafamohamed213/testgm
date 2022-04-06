using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.InventoryViewModels
{
    public class InventoryTablelPagingViewModel<T>
    {
        public List<T> items { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
        public int itemsCount { get; set; }
        public int Tablelength { get; set; }
    }
}
