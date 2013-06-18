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
                cfg.AddProfile<BorrowerModelProfile>();
                cfg.AddProfile<LoanModelProfile>();
            });
        }

        public class BorrowerModelProfile : Profile
        {
            protected override void Configure()
            {
                //Map all viewmodels related to LMS/Borrowers to their corresponding model.
                CreateMap<Borrower,                 CreateBorrowerViewModel>();
                CreateMap<CreateBorrowerViewModel,  Borrower>();
                CreateMap<Borrower,                 EditBorrowerViewModel>();
                CreateMap<EditBorrowerViewModel,    Borrower>();
                CreateMap<Borrower,                 DeleteBorrowerViewModel>();
                CreateMap<DeleteBorrowerViewModel,  Borrower>();
                CreateMap<Borrower,                 ListBorrowerViewModel>();
                CreateMap<ListBorrowerViewModel,    Borrower>();
            }
        }

        public class AssetEditModelProfile : Profile
        {
            protected override void Configure()
            {
                // Mapping for maintain Asset, between domain models and view models
                CreateMap<Asset, AssetEditModel>();
                CreateMap<AssetEditModel, Asset>();

                CreateMap<Repair, AssetRepairCreateModel>();
                CreateMap<AssetRepairCreateModel, Repair>();
                CreateMap<Repair, AssetRepairsHistoryModel>();

                CreateMap<AssignedLocation, AssignedLocationCreateModel>();
                CreateMap<AssignedLocationCreateModel, AssignedLocation>();
                CreateMap<AssignedLocation, LocationHistoryItemsModel>();

                AutoMapper.Mapper.CreateMap<ListEmployeeViewModel, Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower, ListEmployeeViewModel>(); 
                AutoMapper.Mapper.CreateMap<CreateEmployeeViewModel,    Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower,                   CreateEmployeeViewModel>();
                AutoMapper.Mapper.CreateMap<EditEmployeeViewModel,      Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower,                   EditEmployeeViewModel>();
                AutoMapper.Mapper.CreateMap<DeleteEmployeeViewModel,    Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower,                   DeleteEmployeeViewModel>();

                CreateMap<AssignedLocation, LocationHistoryItemsModel>();

                CreateMap<AssignedStatus, AssignedStatusCreateModel>();
                CreateMap<AssignedStatusCreateModel, AssignedStatus>();
                CreateMap<AssignedStatus, AssetStatusHistoryModel>();

                CreateMap<AssetCondition, AssetConditionCreateModel>();
                CreateMap<AssetConditionCreateModel, AssetCondition>();
                CreateMap<AssetCondition, AssetConditionHistoryModel>();
                AutoMapper.Mapper.CreateMap<ListEmployeeViewModel, Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower, ListEmployeeViewModel>(); 
                AutoMapper.Mapper.CreateMap<CreateEmployeeViewModel,    Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower,                   CreateEmployeeViewModel>();
                AutoMapper.Mapper.CreateMap<EditEmployeeViewModel,      Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower,                   EditEmployeeViewModel>();
                AutoMapper.Mapper.CreateMap<DeleteEmployeeViewModel,    Borrower>();
                AutoMapper.Mapper.CreateMap<Borrower,                   DeleteEmployeeViewModel>();

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