using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AlisFirst.Validation;

namespace AlisFirst.ViewModels
{
    public class CreateBorrowerViewModel
    {
        public string Surname { get; set; }
        public string BarCode { get; set; }
        public string GivenName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BorrowerExpiryDate { get; set; }
        [UniqueEmailAttribute]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsEmployee { get; set; }
    }

    public class EditBorrowerViewModel
    {
        [Required]
        public int BorrowerID { get; set; }
        public string Surname { get; set; }
        public string BarCode { get; set; }
        public string GivenName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BorrowerExpiryDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsEmployee { get; set; }
    }

    public class DeleteBorrowerViewModel
    {
        [Required]
        public int BorrowerID { get; set; }
        public string Surname { get; set; }
        public string BarCode { get; set; }
        public string GivenName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BorrowerExpiryDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsEmployee { get; set; }
    }
}