using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DODY.CustomValidation;

namespace DODY.Models
{[MetadataType(typeof(MetadataSale))]
  public partial class Sale
    {
        // Add New Properity
    }
    public class MetadataSale
    {
        // Edit properties
        [Display(Name = "Sno")]
        public int Sno { get; set; }

        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        public string Brand { get; set; }

        [Required]
        public int Order_Count { get; set; }

        [Required]
        public int Sold { get; set; }

        [Required]
        public int Canceled { get; set; }

        public int Year { get; set; }

        [Required]
        [Display(Name = "Total")]
        public Nullable<double> Total { get; set; }

        public int Ono { get; set; }
    }
}