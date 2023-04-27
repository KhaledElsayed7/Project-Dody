using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DODY.CustomValidation;

namespace DODY.Models
{[MetadataType(typeof(MetadataOrder))]
    public partial class Order
    {
        // Add new Properties

    }
    public class MetadataOrder
    {  //Edit Properties
        public int Ono { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "Name Length Should Be Between 1 And 3 Character")]
        public string Size { get; set; }


        //[StringLength(100, MinimumLength = 8, ErrorMessage = "Address Length Should Be Between 8 And 100 Character")]
        [Required]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name Length Should Be Between 3 And 15 Character")]
        public string Color { get; set; }

        [Display(Name = "OrderTime")]
        [DataType(DataType.DateTime)]
        [LessDate]
        public Nullable<System.DateTime> OrderTime { get; set; }


        [Display(Name = "Total")]
        [Range(1, 5000, ErrorMessage = "Quantity Should Be Between 1 And 5000 ")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false)]
        public Nullable<float> Total { get; set; }

        [Display(Name = "Pno")]
        public int Pno { get; set; }

        [Display(Name = "Cid")]
        public int Cid { get; set; }

      

    }
}