using ALP.Application.Entity.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity
{
    /// <summary>
    /// sap工艺路线实体数据
    /// </summary>
    public class SAPBS_Process
    {
        public BS_ProcessEntity Process;
        public List<BS_ProcessOfOperations> ProcessOfOperations;
       
    }
    public class BS_ProcessOfOperations
    {
        public BS_ProcessOfOperationsEntity ProcessOfOperation;
        public List<BS_ProcessOfOperationsAttrEntity> ProcessOfOperationsAttrs;
    }
}
