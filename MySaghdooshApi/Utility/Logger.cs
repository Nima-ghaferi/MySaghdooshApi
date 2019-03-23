using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySaghdooshApi.Utility
{
    class Logger
    {
        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}