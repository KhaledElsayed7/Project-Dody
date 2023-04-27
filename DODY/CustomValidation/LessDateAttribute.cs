using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace DODY.CustomValidation
{
    public class LessDateAttribute:ValidationAttribute
    {
        public LessDateAttribute():base("{0} Date Should Less than Current date")
    {

    }

        public override bool IsValid(object value)
        {
            DateTime PropValue = Convert.ToDateTime(value);
            if (PropValue <= DateTime.Now)
                return true;
            else
                return false;
        }
    }
 

}