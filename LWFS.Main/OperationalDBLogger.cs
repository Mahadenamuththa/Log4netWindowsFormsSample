using log4net;
using log4net.Config;

namespace LWFS.Main
{
    public class OperationalDBLogger
    {
        private static readonly ILog logger = log4net.LogManager.GetLogger("OperationalDBLogAppender");
        #region Constructor
        static OperationalDBLogger()
        {
            XmlConfigurator.Configure();
        }
        #endregion
        #region Public Methods
        public static bool AddError(string message)
        {
            logger.Error(message);
            return true;
        }
        #endregion
    }
}
