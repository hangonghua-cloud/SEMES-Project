using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Data
{
    public class ToolsHelper
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        static extern int UuidCreateSequential(out Guid guid);

        public static Guid GenGuid()
        {
            const int RPC_S_OK = 0;
            Guid guid;
            int result = UuidCreateSequential(out guid);
            if (result != RPC_S_OK)
            {
                throw new ApplicationException("生成GUID失败: " + result);
            }
            return guid;
        }
    }
}
