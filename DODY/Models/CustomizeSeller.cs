using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DODY.CustomValidation;

namespace DODY.Models
{
    [MetadataType(typeof(MetadataSeller))]
    public partial class Seller
    {// add method or new properties
        [Display(Name = "Confirm Password")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Password Length Should Be Between 7 And 20 Character")]
        [Compare("Password", ErrorMessage = "Password is not match")]
        public String ConfPassword { get; set; }

        [Display(Name = "Confirm Email")]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Email Length Should Be Between 12 And 100 Character")]
        [Compare("Email", ErrorMessage = "Email is not match")]
        public String ConfEmail { get; set; }
    }

    public class MetadataSeller
    { //Edit Properties
        public int Sid { get; set; } 

        [Required]
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        public string Fname { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        public string Lname { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Range(01000000000, 01299999999, ErrorMessage = "you Should Enter number Between 01000000000-01299999999")]
        public Nullable<int> Phone { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Address Length Should Be Between 2 And 100 Character")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Email Length Should Be Between 12 And 100 Character")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Password Length Should Be Between 7 And 20 Character")]
        public string Password { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name ="Birth Date")]
        [DataType(DataType.Date)]
        [LessDate]     // Error message is Written by developer in  class called"LessDateAttribute"
        public Nullable<System.DateTime> Birth_Date { get; set; }

        [Required]
        [Display(Name ="Manager Id")]
        public Nullable<int> Mid { get; set; }

    }
}