# Log4netWindowsFormsSample
![ls-logo](https://user-images.githubusercontent.com/21302583/154553875-55d0119f-dba9-4166-bf24-02c5a7612a1b.jpg)
# Apache log4net™
[![Build status](https://ci.appveyor.com/api/projects/status/mgd5hyg6gr6nq1d8/branch/master?svg=true)](https://ci.appveyor.com/project/Mahadenamuththa/log4netwindowsformssample/branch/master)

[![Build history](https://buildstats.info/appveyor/chart/Mahadenamuththa/log4netwindowsformssample)](https://ci.appveyor.com/project/Mahadenamuththa/log4netwindowsformssample/history)


## First of All, What is Apache log4net™?
The Apache log4net library is a tool to help the programmer output log statements to a variety of output targets. log4net is a port of the excellent Apache log4j™ framework to the Microsoft® .NET runtime. We have kept the framework similar in spirit to the original log4j while taking advantage of new features in the .NET runtime. For more information on log4net see the features document.

## How to install

Install Log4net and it's dependencies using NuGet. 

`Install-Package log4net`

Or via the .NET Core CLI:

`dotnet add package log4net `

All versions can be found [here](https://logging.apache.org/log4net/download_log4net.html)

## Create Project

01. First Create Project File->New->Project
02. Select Windows Forms App (.Net Framework) and select Next

![image](https://user-images.githubusercontent.com/21302583/154557003-fd2fe7ef-6de4-4f5a-a224-880772c811e0.png)

03. Project name as `LWFS.Main` and Soluation name as `Log4netWindowsFormsSample`

![image](https://user-images.githubusercontent.com/21302583/154557641-60fca7c1-4a2a-4351-9162-71b6695d6c72.png)

04. Go to Tools->Nuget Package Manager->Manage Nuget Packages For Soluation and Install log4net

![image](https://user-images.githubusercontent.com/21302583/154558081-ce2f4b2f-aba5-420c-a433-d346f477c7f1.png)

05. Go to soluation explorer and Under LWFS.Main project create folder as Files and inside Errors

![image](https://user-images.githubusercontent.com/21302583/154558844-404765f4-6003-4c61-bfd1-fd3e69ad1542.png)

06. Go to App.config and add these lines to there 

```xml
<configSections>
    <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net"/>
</configSections>
<appSettings>
    <add key="log4net.Internal.Debug" value="true"/>
</appSettings>
<log4net debug="true">
    <appender name="RollingLogFileAppender" type="log4net.Appender.RollingFileAppender">
        <file value="Files/Errors/Errors.txt"/>
        <appendToFile value="true"/>
        <rollingStyle value="Size"/>
        <maxSizeRollBackups value="10"/>
        <maximumFileSize value="10MB"/>
        <staticLogFileName value="true"/>
        <layout type="log4net.Layout.PatternLayout">
            <conversionPattern value="%-5p %d %5rms %-22.22c{1} %m%n"/>
        </layout>
    </appender>
    <root>
        <level value="DEBUG"/>
        <appender-ref ref="RollingLogFileAppender"/>
    </root>
</log4net>
```
07.  Create ErrorLogger.cs Class

![image](https://user-images.githubusercontent.com/21302583/154560534-fee2ecbc-2d8d-4bd2-b61f-7b5283078dc4.png)

and change inside as 
```csharp
   public static class ErrorLogger
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ErrorLogger));
        #region Constructor
        static ErrorLogger()
        {
            XmlConfigurator.Configure();
        }
        #endregion

        #region Public Methods
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

                logger.Error(errorDetails);
            }
            return true;
        }
        #endregion
    }
```
