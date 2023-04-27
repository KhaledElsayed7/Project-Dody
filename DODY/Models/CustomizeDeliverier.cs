using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DODY.CustomValidation;

namespace DODY.Models
{
    [MetadataType(typeof(MetadataDeliverier))]
    public class CustomizeDeliverier
    {
        //Add New properties
    }
    public class MetadataDeliverier
    {
        //Modify Properity
        public int Did { get; set; }

        [Required]
        [Display(Name="Full Name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        [DataType(DataType.Text)]
        public string Name { get; set; }


        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Address Length Should Be Between 8 And 100 Character")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Range(01000000000, 01299999999, ErrorMessage = "you Should Enter number Between 01000000000-01299999999")]
        public Nullable<int> Phone { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [LessDate]
        public Nullable<System.DateTime> Birth_Date { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        public int Ono { get; set; }
    }
}