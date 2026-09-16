using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.AuthorizeManage
{
    public class BaseSAPService
    {
        public string getValue(JObject job, string propertyName)
        {
            string result = string.Empty;
            if (job == null || string.IsNullOrEmpty(propertyName)) return result;
            if (job.Property(propertyName) == null) return result;
            return job[propertyName].ToString();
        }
    }
}
