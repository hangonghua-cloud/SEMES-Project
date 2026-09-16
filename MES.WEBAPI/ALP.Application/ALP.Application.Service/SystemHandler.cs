using ALP.Application.Entity.BaseManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.UtilExtend;
using ALP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service
{
    public class SystemHandler
    {
        private static WMSBaseSystemSettingEntity _systemSettingEntity;

        /// <summary>
        /// 初始化系统配置对象
        /// </summary>
        /// <returns></returns>
        private static void InitSystemSetting()
        {
            if (_systemSettingEntity == null)
            {
                var _repository = new RepositoryFactory().BaseRepository();
                _systemSettingEntity = _repository.FindEntity<WMSBaseSystemSettingEntity>(d => 1 == 1);
                if (_systemSettingEntity == null)
                    _systemSettingEntity = new WMSBaseSystemSettingEntity();
            }
        }
    
    }
}
