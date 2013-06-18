//File AlisFirst.Helpers.AutoMapperBootstrapper.cs
using AutoMapper;
using AlisFirst.Models;
using AlisFirst.Areas.AMS.ViewModels;
using AlisFirst.Areas.LMS.ViewModels;
using System.Collections.Generic;

namespace AlisFirst.Helpers
{
    public class AutoMapperBootstrapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AssetEditModelProfile>();
                cfg.AddProfile<EmployeeModelProfile>();
            });
        }

        public class EmployeeModelProfile : Profile
        {
            protected override void Configure()
            {
                AutoMapper.Mapper.CreateMap<ListEmployeeViewModel,      Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower,                   ListEmployeeViewModel>();
                AutoMapper.Mapper.CreateMap<CreateEmployeeViewModel,    Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower,                   CreateEmployeeViewModel>();
                AutoMapper.Mapper.CreateMap<EditEmployeeViewModel,      Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower,                   EditEmployeeViewModel>();
                AutoMapper.Mapper.CreateMap<DeleteEmployeeViewModel,    Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower,                   DeleteEmployeeViewModel>();               
            }
        }

        public class AssetEditModelProfile : Profile
        {
            protected override void Configure()
            {
                // Mapping for maintain Asset, between domain models and view models
                CreateMap<Asset, AssetMaintainModel.AssetEditVM>();
                CreateMap<AssetMaintainModel.AssetEditVM, Asset>();

                CreateMap<Repair, AssetRepairCreateModel>();
                CreateMap<AssetRepairCreateModel, Repair>();

                CreateMap<Repair, AssetMaintainModel.AssetRepairsHistoryModel>();

                CreateMap<AssignedLocation, AssignedLocationCreateModel>();
                CreateMap<AssignedLocationCreateModel, AssignedLocation>();

                CreateMap<AssignedLocation, AssetMaintainModel.LocationHistoryItemsModel>();

                //// Mapping for maintain Asset, between domain models and view models
                //AutoMapper.Mapper.CreateMap<AssignedLocation, AssetEdit.CreateAssignedLocationVM>();
                //AutoMapper.Mapper.CreateMap<AssetEdit.CreateAssignedLocationVM, AssignedLocation>();
                //AutoMapper.Mapper.CreateMap<AssignedLocation, AssetEdit.LocationHistoryItemsVM>();

            }
        }
    }
}