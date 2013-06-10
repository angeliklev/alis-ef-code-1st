//File AlisFirst.Helpers.AutoMapperBootstrapper.cs
using AutoMapper;
using AlisFirst.Models;
using AlisFirst.Areas.AMS.ViewModels;

namespace AlisFirst.Helpers
{
    public class AutoMapperBootstrapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AssetEditModelProfile>();
            });
        }

        public class AssetEditModelProfile : Profile
        {
            protected override void Configure()
            {
                // Mapping for maintain Asset, between domain models and view models
                CreateMap<Asset, AssetMaintainModel.AssetEditVM>();
                CreateMap<AssetMaintainModel.AssetEditVM, Asset>();

                CreateMap<Repair, AssetMaintainModel.AssetRepairCreateModel>();
                CreateMap<AssetMaintainModel.AssetRepairCreateModel, Repair>();

                CreateMap<AssignedLocation, AssetMaintainModel.AssignedLocationCreateModel>();
                CreateMap<AssetMaintainModel.AssignedLocationCreateModel, AssignedLocation>();

                CreateMap<AssignedLocation, AssetMaintainModel.LocationHistoryItemsModel>();
                CreateMap<Repair, AssetMaintainModel.AssetRepairsHistoryModel>();

                //// Mapping for maintain Asset, between domain models and view models
                //AutoMapper.Mapper.CreateMap<Asset, AssetEdit.AssetEditVM>();
                //AutoMapper.Mapper.CreateMap<AssetEdit.AssetEditVM, Asset>();
                //AutoMapper.Mapper.CreateMap<Repair, AssetEdit.CreateAssetRepairVM>();
                //AutoMapper.Mapper.CreateMap<AssetEdit.CreateAssetRepairVM, Repair>();
                //AutoMapper.Mapper.CreateMap<AssignedLocation, AssetEdit.CreateAssignedLocationVM>();
                //AutoMapper.Mapper.CreateMap<AssetEdit.CreateAssignedLocationVM, AssignedLocation>();
                //AutoMapper.Mapper.CreateMap<AssignedLocation, AssetEdit.LocationHistoryItemsVM>();

            }
        }
    }
}