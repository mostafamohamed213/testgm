using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel.InventoryViewModels
{
    public class InventoryItemTypePlusInventoryItemViewModel
    {
        public InventoryItemTypePlusInventoryItemViewModel()
        {
            InventoryItemType = new InventoryItemTypeViewModel();
            InventoryItem = new InventoryItemViewModel();
        }
        public InventoryItemTypeViewModel InventoryItemType { get; set; }
        public InventoryItemViewModel InventoryItem { get; set; }
    }
}
