using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DODY.Models
{
    [MetadataType(typeof(metadataproduct))]
    public partial class Product
    { 
        // add method or add new properties

    }
    public class metadataproduct
    {
        // Edit properties
        [Display(Name = "No")]
        public int Pno { get; set; } 

        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        public string Color { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "Name Length Should Be Between 1 And 3 Character")]
        public string Size { get; set; }
        
        [Required]
        [Display(Name ="Product for")]
        [DataType(DataType.Text)]
        public string Pro_For { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "Name Length Should Be Between 1 And 300 Character")]
        public string Description { get; set; }

        [Required]
        [Display(Name="Department")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Range(1, 5000, ErrorMessage = "Quantity Should Be Between 1 And 5000 ")]
        [DisplayFormat(DataFormatString ="{0:c}" ,ApplyFormatInEditMode =false)]
        public Nullable<double> UnitPrice { get; set; }

        [Required]
        [Display(Name = "Brand")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        public string Brand { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set;}
    }
}