using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AlisFirst.Models;

namespace AlisFirst.Areas.AMS.ViewModels
{
    public class AssignedLocationCreateModel
    {
        [HiddenInput(DisplayValue = false)]
        public int AssetID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime AssignedLocationDate { get; set; }

        [Required]
        public int? LocationID { get; set; }
        public virtual Location Location { get; set; }
 //       public AssignedLocationCreateBasicModel AssignedLocation { get; set; }
        public SelectList LocationsList { get; private set; }

        public AssignedLocationCreateModel() { }

        public AssignedLocationCreateModel(int id, IEnumerable locations)
        {
            //AssignedLocation = new AssignedLocationCreateBasicModel
            //{
                AssetID = id;
                AssignedLocationDate = DateTime.Today;
            //};
            LocationsList = new SelectList(locations, "LocationID", "LocationName", String.Empty);
        }
    }

    public class AssignedLocationCreateBasicModel
    {

    }
}