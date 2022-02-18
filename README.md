# Log4netWindowsFormsSample
![ls-logo](https://user-images.githubusercontent.com/21302583/154553875-55d0119f-dba9-4166-bf24-02c5a7612a1b.jpg)
# Apache log4net™
[![Build status](https://ci.appveyor.com/api/projects/status/mgd5hyg6gr6nq1d8/branch/master?svg=true)](https://ci.appveyor.com/project/Mahadenamuththa/log4netwindowsformssample/branch/master)

[![Build history](https://buildstats.info/appveyor/chart/Mahadenamuththa/log4netwindowsformssample)](https://ci.appveyor.com/project/Mahadenamuththa/log4netwindowsformssample/history)


## First of All, What is Apache log4net™?
The Apache log4net library is a tool to help the programmer output log statements to a variety of output targets. log4net is a port of the excellent Apache log4j™ framework to the Microsoft® .NET runtime. We have kept the framework similar in spirit to the original log4j while taking advantage of new features in the .NET runtime. For more information on log4net see the features document.

## Features
* Support for multiple frameworks
* Output to multiple logging targets
* Hierarchical logging architecture
* XML Configuration
* Dynamic Configuration
* Logging Context
* Proven architecture
* Modular and extensible design
* High performance with flexibility

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

                logger.Error(errorDetails);
            }
            return true;
        }
        #endregion
    }
```
08.  Lets create error and see ho it's works.Go to your Home form and add button as Add Error,Text box to check error as below
 
![image](https://user-images.githubusercontent.com/21302583/154749618-06ee3b61-6503-4ea4-961c-b6b3ca963d3f.png)

09. now double click on button and change code 
```csharp
using System;
using System.Windows.Forms;

namespace LWFS.Main
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                throw new Exception(textBox1.Text);
            }
            catch (Exception ex)
            {
                ErrorLogger.AddError(ex);
                throw;
            }
        }
    }
}
```
10.Now run your windows form application and enter some text on your text box

![image](https://user-images.githubusercontent.com/21302583/154750641-eebd5492-4ba7-4ead-bf2c-113f8a13a783.png)

11. Go to your `Log4netWindowsFormsSample\LWFS.Main\bin\Debug\Files\Errors` folder inside your project and open Errors.txt
12.Now you your Error message logged there
```text
ERROR 2022-02-19 01:06:46,209    68ms ErrorLogger            
Class name: Home
Method name: button1_Click
Line number: 17
Exception message: Test 1
   at LWFS.Main.Home.button1_Click(Object sender, EventArgs e) in D:\Projects\GitHub Projects\Log4netWindowsFormsSample\LWFS.Main\Home.cs:line 17
```

## Now see how to configure another way to configure log4net in separate config file.
01. Add config file name as `log4net.config`in your project and paste this code.

```xml
<log4net>
    <root>
        <level value="ALL" />
        <appender-ref ref="console" />
        <appender-ref ref="file" />
    </root>
    <appender name="console" type="log4net.Appender.ConsoleAppender">
        <layout type="log4net.Layout.PatternLayout">
            <conversionPattern value="%date %level %logger - %message%newline" />
        </layout>
    </appender>
    <appender name="file" type="log4net.Appender.RollingFileAppender">
        <file value="myapp.log" />
        <appendToFile value="true" />
        <rollingStyle value="Size" />
        <maxSizeRollBackups value="5" />
        <maximumFileSize value="10MB" />
        <staticLogFileName value="true" />
        <layout type="log4net.Layout.PatternLayout">
            <conversionPattern value="%date [%thread] %level %logger - %message%newline" />
        </layout>
    </appender>
</log4net>
```

02. Add new class ErrorLoggerType2.cs and change code same as previous errorlog but here we change one thing
```csharp
private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
```

```csharp
public class ErrorLoggerType2
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Constructor
        static ErrorLoggerType2()
        {
            XmlConfigurator.Configure();
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
```
03. Now go to your `Home.cs` and change code as below
```csharp
private void button1_Click(object sender, EventArgs e)
{
    try
    {
        throw new Exception(textBox1.Text);
    }
    catch (Exception ex)
    {
        ErrorLogger.AddError(ex);
        ErrorLoggerType2.AddError(ex);
    }
}
```
04. Now run your windows form application and enter some text on your text box

05 Go to your `Log4netWindowsFormsSample\LWFS.Main\bin\Debug\Files\Errors` folder inside your project and open Errors.txt

``Here Download Errors.txt`` [Errors.txt](https://github.com/Mahadenamuththa/IBMMQSample/files/8100726/Errors.txt)

```text
ERROR 2022-02-19 03:22:56,420 51045ms ErrorLogger            
Class name: Home
Method name: button1_Click
Line number: 17
Exception message: Test2
   at LWFS.Main.Home.button1_Click(Object sender, EventArgs e) in D:\Projects\GitHub Projects\Log4netWindowsFormsSample\LWFS.Main\Home.cs:line 17

INFO  2022-02-19 03:22:56,420 51045ms ErrorLoggerType2       
Class name: Home
Method name: button1_Click
Line number: 17
Exception message: Test2
   at LWFS.Main.Home.button1_Click(Object sender, EventArgs e) in D:\Projects\GitHub Projects\Log4netWindowsFormsSample\LWFS.Main\Home.cs:line 17
```
### Like this you can add 
* Debug
* Info
* Warn
* Error

## You Can Make Your Own Custom log4net Appenders

01. Add new appender (`OperationLogAppender`) to your `log4net.config`.
```xml
<appender name="OperationLogAppender" type="log4net.Appender.RollingFileAppender">
    <file value="Files/Operations/Logger.txt"/>
    <appendToFile value="true" />
    <rollingStyle value="Size" />
    <maxSizeRollBackups value="5" />
    <maximumFileSize value="10MB" />
    <staticLogFileName value="true" />
    <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%-5p %d %5rms %-22.25c{1} %m%n"/>
    </layout>
</appender>
<logger name="OperationLogFileAppender">
    <level value="Info"/>
    <appender-ref ref="OperationLogAppender"/>
</logger>
```
02. Now add new class named `OperationLogger.cs` and change code there as below
```csharp
    public class OperationLogger
    {
        private static readonly ILog logger = LogManager.GetLogger("OperationLogFileAppender");

        #region Constructor
        static OperationLogger()
        {
            XmlConfigurator.Configure();
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
```

03. Now go to your `Home.cs` and change code as below

```csharp
private void button1_Click(object sender, EventArgs e)
{
    try
    {
        OperationLogger.LogMessage(textBox1.Text);
        throw new Exception(textBox1.Text);
    }
    catch (Exception ex)
    {
        ErrorLogger.AddError(ex);
        ErrorLoggerType2.AddError(ex);
    }
}
```

04. Now run your windows form application and enter some text on your text box
05. Now you seen in your folder `Files\Operations` and file created there as Logger [Logger.txt](https://github.com/Mahadenamuththa/Log4netWindowsFormsSample/files/8100946/Logger.txt)

## Now we are going to Log in Database
01. Frist create a new database `log4netDatabase` for this.
![image](https://user-images.githubusercontent.com/21302583/154773315-c215a8a1-25a2-4169-ab98-1d564c6973dc.png)

02. Create table OperationalLog and create fields as below
![image](https://user-images.githubusercontent.com/21302583/154773678-b642a1f0-dfed-4ae6-9af7-83ac17741665.png)
```sql
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OperationalLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[DateLogged] [datetime] NULL,
 CONSTRAINT [PK_OperationalLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
```
03. now go to your log4net.config add this 
```xml
<!--Database Listener-->
<appender name="AdoNetAppender" type="log4net.Appender.AdoNetAppender">
    <bufferSize value="1"/>
    <connectionType value="System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"/>
    <connectionStringName value="DefaultConnection"/>
    <commandText value="INSERT INTO dbo.OperationalLog ([Message],[DateLogged]) VALUES (@message,@dateLogged)"/>
    <parameter>
        <parameterName value="@message"/>
        <dbType value="String"/>
        <size value="4000"/>
        <layout type="log4net.Layout.PatternLayout">
            <conversionPattern value="%message"/>
        </layout>
    </parameter>
    <parameter>
        <parameterName value="@dateLogged"/>
        <dbType value="DateTime"/>
        <layout type="log4net.Layout.RawTimeStampLayout"/>
    </parameter>
</appender>
<logger name="OperationalDBLogAppender">
    <level value="Info"/>
    <appender-ref ref="AdoNetAppender"/>
</logger>
```

04. Add connection string to your App.config
```xml
<connectionStrings>
    <add name="DefaultConnection" connectionString="Data Source=YourServerName;Initial Catalog=log4netDatabase;User ID=sa;Password=123;Connection Timeout=60;multipleactiveresultsets=True;" providerName="System.Data.SqlClient"/>
</connectionStrings>
```

05. Create new class file `OperationalDBLogger.cs` and change code as below
```csharp
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
```
06. Now go to your `Home.cs` and change code as below

```csharp
private void button1_Click(object sender, EventArgs e)
{
    try
    {
        OperationLogger.LogMessage(textBox1.Text);
	OperationalDBLogger.AddError(textBox1.Text);
        throw new Exception(textBox1.Text);
    }
    catch (Exception ex)
    {
        ErrorLogger.AddError(ex);
        ErrorLoggerType2.AddError(ex);
    }
}
```

07. Now run your windows form application and enter some text on your text box and check your Table :D
![image](https://user-images.githubusercontent.com/21302583/154775562-29c188cf-e9d1-47c3-83e4-6e38dc4c654e.png)
