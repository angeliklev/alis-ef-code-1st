using AlisFirst.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssetCheckListEditModel
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }
        public List<SelectedCheckListItemsData> SelectedItems { get; set; }

        private readonly ICheckListItemRepository checkitemsRepo = new CheckListItemRepository();
        private readonly IAssetRepository assetRepo = new AssetRepository();

        public AssetCheckListEditModel() { }
        public AssetCheckListEditModel(int id)
        {
            var allCheckListItems = checkitemsRepo.All.ToList();
            var asset = assetRepo.Find(id);
            // check if it is not null!!!

            var assetCheckListItems = new HashSet<int>(asset.CheckListItems.Select(c => c.CheckListItemID));
            AssetID = id;
            SelectedItems = new List<SelectedCheckListItemsData> { };

            foreach (var item in allCheckListItems)
            {
                SelectedItems.Add(new SelectedCheckListItemsData
                {
                    CheckListItemID = item.CheckListItemID,
                    ItemName = item.CheckListItemName,
                    Selected = assetCheckListItems.Contains(item.CheckListItemID)
                });
            };
        }
    }

    public class SelectedCheckListItemsData
    {
        public int CheckListItemID { get; set; }
        public string ItemName { get; set; }
        public bool Selected { get; set; }
    }
}