using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALP.Application.WebApi.APIModel
{
    public class ApiErrors
    {
        public string Id { get; set; }
        public IList<ApiValidationFailure> Errors { get; set; }

    }
}