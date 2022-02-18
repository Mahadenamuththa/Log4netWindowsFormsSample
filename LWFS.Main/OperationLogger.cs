using log4net;
using log4net.Config;
using System;

namespace LWFS.Main
{
    public class OperationLogger
    {
        private static readonly ILog logger = LogManager.GetLogger("OperationLogFileAppender");

        #region Constructor
        static OperationLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        #endregion
        #region Public Methods
        public static bool LogMessage(string message)
        {
            logger.Info(string.Concat(message));
            return true;
        }
        public static bool LogWarning(string message)
        {
            logger.Warn(message);
            return true;
        }
        public static bool LogError(string message)
        {
            logger.Error(message);
            return true;
        }
        public static bool LogError(string message, Exception ex)
        {
            logger.Error(message + " Message Details: " + ex.Message + Environment.NewLine + ex.StackTrace);
            return true;
        }
        #endregion
    }
}
