using ALP.Application.Entity.Material;
using Lib.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Bll
{
    public class BaseMaterialBll
    {
        BaseMaterialDal dal = new BaseMaterialDal();

        /// <summary>
        /// 查询物料主数据实体
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public Base_MaterialEntity GetEntity(string materialCode)
        {
            return dal.GetEntity(materialCode);
        }
    }
}
