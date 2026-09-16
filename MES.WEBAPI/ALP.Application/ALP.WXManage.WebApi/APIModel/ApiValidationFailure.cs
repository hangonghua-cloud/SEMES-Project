using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALP.Application.WebApi.APIModel
{
    public class ApiValidationFailure
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}