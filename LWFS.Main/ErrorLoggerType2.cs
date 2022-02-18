using log4net.Config;
using System;
using System.Diagnostics;
using System.Reflection;

namespace LWFS.Main
{
    public class ErrorLoggerType2
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Constructor
        static ErrorLoggerType2()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add Error
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>boolean</returns>
        public static bool AddError(Exception ex)
        {
            if (ex != null)
            {
                StackTrace exTrace = new StackTrace(ex, true);
                // Class name
                string className = string.Format("Class name: {0}", exTrace.GetFrame(exTrace.FrameCount - 1).GetMethod().ReflectedType.Name);
                // Method name
                string methodName = string.Empty;
                if (exTrace.GetFrame(exTrace.FrameCount - 1).GetMethod().MemberType == MemberTypes.Method)
                {
                    methodName = string.Format("Method name: {0}", exTrace.GetFrame(exTrace.FrameCount - 1).GetMethod().Name);
                }
                else
                {
                    methodName = string.Format("Method name: {0}", exTrace.GetFrame(exTrace.FrameCount - 1).GetMethod().MemberType.ToString());
                }
                // Line number
                string lineNumber = string.Format("Line number: {0}", exTrace.GetFrame(exTrace.FrameCount - 1).GetFileLineNumber());
                // Exception message
                string exception = string.Format("Exception message: {0}", ex.Message + Environment.NewLine + ex.StackTrace);
                // Inner exception message
                string innerException = ex.InnerException != null ? Environment.NewLine + string.Format("Inner exception: {0}", ex.InnerException) : string.Empty;


                string errorDetails = Environment.NewLine + className
                                    + Environment.NewLine + methodName
                                    + Environment.NewLine + lineNumber
                                    + Environment.NewLine + exception
                                    + innerException
                                    + Environment.NewLine;
                log.Info(errorDetails);
            }
            return true;
        }
        #endregion
    }
}

