using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Quartz.Logging.OperationName;

namespace ALP.QuartzTask.Util
{
   public class LoggerManager
    {
        private ILog _logger = LogManager.GetLogger(typeof(Job));
        public LoggerManager()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
        }

        public void Write(string message)
        {
            _logger.InfoFormat(message);
        }
    }
}
