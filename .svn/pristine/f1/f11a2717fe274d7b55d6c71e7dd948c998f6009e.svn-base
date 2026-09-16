using ALP.Application.Code;
using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.Service.BaseManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.BaseManage
{
    public class BaseUserLineBLL
    {
        private IBaseUserLineService service = new BaseUserLineService();

        public void SaveUserLines(BaseUserLineEntity entity)
        {
            string[] lineCodes = entity.LineCode.Split(",");
            try
            {
                List<string> paramalarmList = new List<string>();
                if (lineCodes.Length > 0)
                {
                    paramalarmList.Add("DELETE Base_UserLineRelation WHERE UserId='"+ entity.UserId + "' ");
                    for (int i = 0; i < lineCodes.Length; i++)
                    {
                        string lineCode = lineCodes[i];
                        string sql = @"INSERT INTO Base_UserLineRelation(Id, UserId, [LineCode], CreateUserId, CreateTime)
                                                        SELECT NEWID(), '"+entity.UserId+"', '"+ lineCode+ "', '"+ OperatorProvider.Provider.Current().UserId + "', getdate();";
                        paramalarmList.Add(sql);
                    }
                    service.InsertBaseUserLine(paramalarmList);
                    paramalarmList.Clear();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetListTable(string userId)
        {
            return service.GetListTable(userId);
        }

        public IEnumerable<BaseUserLineEntity> GetListEntity(string userId)
        {
            return service.GetListEntity(userId);
        }

    }
}
