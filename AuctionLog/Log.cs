using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace AuctionLog
{
    public static class Log
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogError(Exception ex)
        {
            //logger.Error("Stack trace: " + ex.StackTrace + "; \r\n Message : " + ex.Message, ex);
            logger.Error(ex, "Stack trace: " + ex.StackTrace + "; \r\n Message : " + ex.Message, null);
        }

        public static void LogInfo(string message)
        {
            logger.Info(message);
        }
    }
}
