using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DODY.CustomValidation;



namespace DODY.Models
{
    [MetadataType(typeof(MetadataManager))]
    public partial class Manager
    {
        // add new properties or methods
    }
    public class MetadataManager
    { // Edit properties

        public int Mid { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3-15 Character")]
        public string Fname { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3-15 Character")]
        public string Lname { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Range(01000000000,01299999999,ErrorMessage ="you Should Enter number Between 01000000000-01299999999")]  // won,t solve the problem we need 015
        public Nullable<int> Phone { get; set; }                          // [RegularExpression()]  use it for { phone , password , Email , .....}

        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Address Length Should Be Between 8 And 100 Character")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression(@"\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b", ErrorMessage ="not valid Email")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Email Length Should Be Between 12 And 30 Character")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]                                                                                           
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Password Length Should Be Between 7 And 20 Character")]
        public string Password { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [LessDate]
        public Nullable<System.DateTime> BirthDate { get; set; }
    }
}   