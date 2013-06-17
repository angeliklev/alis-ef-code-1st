//File AlisFirst.Helpers.AutoMapperBootstrapper.cs
using AutoMapper;
using AlisFirst.Models;
using AlisFirst.Areas.AMS.ViewModels;
using System.Collections.Generic;
using AlisFirst.Areas.LMS.ViewModels;

namespace AlisFirst.Helpers
{
    public class AutoMapperBootstrapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AssetEditModelProfile>();
                cfg.AddProfile<LoanModelProfile>();
            });
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
        public class LoanModelProfile : Profile
        {
            protected override void Configure()
            {
                AutoMapper.Mapper.CreateMap<CreateLoanViewModel, Loan>();
                AutoMapper.Mapper.CreateMap<Loan, CreateLoanViewModel>();
                AutoMapper.Mapper.CreateMap<EditLoanViewModel, Loan>();
                AutoMapper.Mapper.CreateMap<Loan, EditLoanViewModel>();
                //base.Configure();
            }
        }
    }
}