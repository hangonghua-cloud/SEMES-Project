using ALP.Application.Entity.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Dal
{
    public class BaseMaterialDal
    {
        /// <summary>
        /// 查询物料主数据实体
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public Base_MaterialEntity GetEntity(string materialCode)
        {
            string sql = @"SELECT Id,
                               MaterialCode,
                               MaterialName,
                               Spec,
                               MaterialClass,
                               SmallClass,
                               Unit,
                               UnitName,
                               IsEnabled,
                               Creator,
                               CreateTime,
                               ModifyBy,
                               ModifyTime
                        FROM dbo.Base_Material
                        WHERE MaterialCode=@MaterialCode";
            return DapperHelper<Base_MaterialEntity>.QuerySingleOrDefault(sql, new { MaterialCode = materialCode });
        }
    }
}
