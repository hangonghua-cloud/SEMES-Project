using ALP.Application.Entity.BaseManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.BaseManage
{
    public interface IBaseAccountService
    {
        IEnumerable<MESBaseAccountEntity> LoadAccountList(string firstName, string lastName, string userEnCode, Pagination pagination, string userCode);

        (bool result, string msg) SaveEntity(string keyValue, MESBaseAccountEntity entity);
    }
}
