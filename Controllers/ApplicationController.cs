using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mvpApi.Controllers
{
    public class ApplicationController : Controller
    {    
        public bool CheckValidations(Object Object, ref List<ValidationResult> ValidationResults)
        {
            ValidationContext contexts = new ValidationContext(Object, null, null);
            var isValid = Validator.TryValidateObject(Object, contexts, ValidationResults, true);

            return isValid;
        }
    }
}

