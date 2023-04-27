using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DODY.CustomValidation;


namespace DODY.Models
{[MetadataType(typeof(MetadataClient))]
    public partial class Client
    {
        // Add new Properties
        
    }

    public class MetadataClient
    {  //Edit Properties
        public int Cid { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        [DataType(DataType.Text)]
        public string Fname { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        [DataType(DataType.Text)]
        public string Lname { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Range(01000000000, 01299999999, ErrorMessage = "you Should Enter number Between 01000000000-01299999999")]
        public Nullable<int> Phone { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Address Length Should Be Between 8 And 100 Character")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [StringLength(20, MinimumLength = 12, ErrorMessage = "Email Length Should Be Between 12 And 100 Character")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required (ErrorMessage ="Enter Password")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Password Length Should Be Between 7 And 20 Character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name ="Birth Date")]
        [DataType(DataType.Date)]
        [LessDate]
        public Nullable<System.DateTime> Birth_Date { get; set; }

        public Nullable<int> Sid { get; set; }

    }
}