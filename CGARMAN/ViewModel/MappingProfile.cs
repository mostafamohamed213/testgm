using AutoMapper;
using CGARMAN.ViewModel.InventoryViewModels;
using CGARMAN.ViewModel.TechnicianViewModels;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGARMAN.ViewModel
{
    public class MappingProfile : Profile
    {
        //IUnitOfWork unitOfWork;
        public MappingProfile(/*IUnitOfWork _unitOfWork*/)
        {
            //unitOfWork = _unitOfWork;
            // include ("InventoryItemType","InventoryItemStatus","Brand" ,Vendor)
            CreateMap<InventoryItem,InventoryItemIndexViewModel>()
                .ForMember(d=>d.InventoryItemId,s=>s.MapFrom(c=>c.InventoryItemId))
                .ForMember(d => d.InventoryItemTypeId, s => s.MapFrom(c => c.InventoryItemTypeId))
                .ForMember(d => d.Name, s => s.MapFrom(c => c.InventoryItemType.Name.Trim()))
                .ForMember(d => d.Quantity, s => s.MapFrom(c => c.Quantity))
                .ForMember(d => d.InventoryItemId,s=>s.MapFrom(c=>c.InventoryItemId))
                .ForMember(d => d.SerialNumber, s => s.MapFrom(c => c.SerialNumber.Trim()))
                .ForMember(d => d.Status, s => s.MapFrom(c => c.InventoryItemStatus.Name.Trim()))               
                .ForMember(d => d.Supplier, s => s.MapFrom(c => c.Vendor.Name.Trim()))
                .ReverseMap();
            CreateMap<InventoryItemType, InventoryItemTypeViewModel>()
                .ForMember(d => d.Brand, s => s.MapFrom(c => c.Brand.Name.Trim()))
                .ForMember(d => d.Model, s => s.MapFrom(c => c.Model.Name.Trim()))
                .ForMember(d => d.Warehouse, s => s.MapFrom(c => c.Warehouse.Name.Trim()))
                .ReverseMap();

            CreateMap<InventoryItemType, InventoryItemTypeIndexViewModel>()
               .ForMember(d => d.Classification, s => s.MapFrom(c => c.InventoryItemTypeClassification.Name.Trim()))
               .ForMember(d => d.Unit, s => s.MapFrom(c => c.InventoryItemTypeUnit.Name.Trim()))
               .ForMember(d => d.Brand, s => s.MapFrom(c => c.Brand.Name.Trim()))
                .ForMember(d => d.Model, s => s.MapFrom(c => c.Model.Name.Trim()))
                .ForMember(d => d.Warehouse, s => s.MapFrom(c => c.Warehouse.Name.Trim()))
               .ReverseMap();
            CreateMap<InventoryItemType, ViewItemTypeViewModel>()
             .ForMember(d => d.Classification, s => s.MapFrom(c => c.InventoryItemTypeClassification.Name.Trim()))
             .ForMember(d => d.Unit, s => s.MapFrom(c => c.InventoryItemTypeUnit.Name.Trim()))
             .ForMember(d => d.Brand, s => s.MapFrom(c => c.Brand.Name.Trim()))
             .ForMember(d => d.Model, s => s.MapFrom(c => c.Model.Name.Trim()))
             .ForMember(d => d.Warehouse, s => s.MapFrom(c => c.Warehouse.Name.Trim()))
             .ForMember(d => d.Category, s => s.MapFrom(c => c.Inventoryitemcategory.Name.Trim()))             
             .ReverseMap();
            CreateMap<InventoryItem, ViewItemViewModel>()
             .ForMember(d => d.Vendor, s => s.MapFrom(c => c.Vendor.Name.Trim()))
             .ForMember(d => d.InventoryItemStatus, s => s.MapFrom(c => c.InventoryItemStatus.Name.Trim()))
             .ForMember(d => d.InventoryLocation, s => s.MapFrom(c => c.InventoryLocationId.HasValue ? "(" + c.InventoryLocation.Warehouse.Name.Trim() + ") -> (" + c.InventoryLocation.ParentInventoryLocation.ParentInventoryLocation.Name.Trim() + ") -> (" + c.InventoryLocation.ParentInventoryLocation.Name.Trim() + ") -> (" + c.InventoryLocation.Name.Trim() + ")" : c.InventoryItemType.Warehouse.Name.Trim()))
             .ForMember(d => d.CodeType, s => s.MapFrom(c => c.CodeType.Name.Trim()));

            CreateMap<CreateInventoryItemTypeViewModel, InventoryItemType>().ReverseMap();
            CreateMap<InventoryItem, InventoryItemWarehouseSpareparts>().ReverseMap();
            CreateMap<InventoryItem, InventoryItemWarehouseTires>().ReverseMap();
            CreateMap<InventoryItem, InventoryItemWarehouseOils>().ReverseMap();
            CreateMap<CreateTechnicianViewModel, Technician>().ReverseMap();

        }

    }
}
