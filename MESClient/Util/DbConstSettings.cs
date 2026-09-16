using System.Configuration;

namespace MESClient
{
    public class DbConstSettings
    {
        private static string Type = Lib.Common.Language.GetLocation();

        public static string BaseDbString
        {
            get
            {
                var conns = ConfigurationManager.ConnectionStrings["BaseDb" + "_" + (string.IsNullOrEmpty(Type) ? "CN" : Type)]?.ToString();
                return conns;
            }
        }

    }
}