using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Security;
using System.Text;

namespace CustomInstaller
{
    /// <summary>
    /// Declares methods for writing information to the event log
    /// </summary>
    public interface ILogger : IDisposable
    {
        void WriteToLog(LogCategory category, string information);
        void WriteToLog(LogCategory category, Exception ex);
        void WriteToLog(LogCategory category, string errorMessage, string stackTrace);
    }

    /// <summary>
    /// This class provides an interaction with the eventlog class to create entries 
    /// into  Windows Applcation event log under 'Chemistry Add-in for Word' event source
    /// </summary>

    #region Error Logger Class

    public sealed class Logger : ILogger
    {
        private const string EVENTSOURCE = "Chemistry Add-in for Word";
        private const string EVENTLOG = "Application";
        private static string SPACE_STRING = " ";
        private bool disposed;

        #region Event Log Entries
        /// <summary>
        /// Writes a message into Event Log as a Error.
        /// </summary>
        /// <param name="message">Descriptiive Error message to be written into Event Log with PROPER format</param>
        private static void CreateErrorEventLogEntry(StringBuilder message)
        {
            try
            {
                using (EventLog eventLog = new EventLog(EVENTLOG))
                {
                    // Create an EventLog instance and assign its source.            
                    eventLog.Source = EVENTSOURCE;
                    // Write an error entry to the event log.    
                    eventLog.WriteEntry(message.ToString(), EventLogEntryType.Error);
                }
            }
            catch (InvalidEnumArgumentException ex)
            {
                LogDebugMessage(ex, message.ToString());
            }
            catch (ArgumentException ex)
            {
                LogDebugMessage(ex, message.ToString());
            }
            catch (InvalidOperationException ex)
            {
                LogDebugMessage(ex, message.ToString());
            }
            catch (Win32Exception ex)
            {
                LogDebugMessage(ex, message.ToString());
            }
        }

        /// <summary>
        /// Writes a message into Event Log as an Information.
        /// </summary>
        /// <param name="message">Descriptiive Information message to be written into Event Log with PROPER format</param>
        private static void CreateInformationEventLogEntry(StringBuilder message)
        {
            try
            {
                using (EventLog eventLog = new EventLog(EVENTLOG))
                {
                    // Create an EventLog instance and assign its source.            
                    eventLog.Source = EVENTSOURCE;
                    // Write an Information entry to the event log.    
                    eventLog.WriteEntry(message.ToString(), EventLogEntryType.Information);
                }
            }
            catch (InvalidEnumArgumentException ex)
            {
                LogDebugMessage(ex, message.ToString());
            }
            catch (ArgumentException ex)
            {
                LogDebugMessage(ex, message.ToString());
            }
            catch (InvalidOperationException ex)
            {
                LogDebugMessage(ex, message.ToString());
            }
            catch (Win32Exception ex)
            {
                LogDebugMessage(ex, message.ToString());
            }
        }
        #endregion

        #region Message Format

        /// <summary>
        /// Gets an information message to be written in proper format.
        /// </summary>
        /// <param name="category">Log Message Category</param>
        /// <param name="information">Log Information</param>
        /// <returns>
        /// properly formatted message as below:
        /// Category:
        /// Innforamtion:
        /// </returns>
        private static StringBuilder GetProperMessageFormat(LogCategory category, string information)
        {
            StringBuilder messageBuilder = null;
            try
            {
                messageBuilder = new StringBuilder(string.Empty);
                messageBuilder.Append(Resource.EX_CATEGORY + SPACE_STRING + category.ToString());

                messageBuilder.Append(Environment.NewLine);
                messageBuilder.Append(Environment.NewLine);

                messageBuilder.Append(Resource.EX_INFORMATION);
                messageBuilder.Append(Environment.NewLine);
                messageBuilder.Append(information);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                LogDebugMessage(exception);
            }
            catch (ArgumentException exception)
            {
                LogDebugMessage(exception);
            }
            return messageBuilder;
        }

        /// <summary>
        /// Gets an error message to be written in proper format.
        /// </summary>
        /// <param name="category">Log Message Category</param>
        /// <param name="ex">Exception from which information needs to be written to Event Log</param>
        /// <returns>
        /// properly formatted message as below:
        /// Category:
        /// Error Message:
        /// Stack Trace:
        /// </returns>
        private static StringBuilder GetProperMessageFormat(LogCategory category, Exception ex)
        {
            StringBuilder messageBuilder = null;
            try
            {
                messageBuilder = new StringBuilder(string.Empty);
                messageBuilder.Append(Resource.EX_CATEGORY + SPACE_STRING + category.ToString());
                messageBuilder.Append(Environment.NewLine);
                messageBuilder.Append(Environment.NewLine);
                AppendExceptionDetails(ex, messageBuilder);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                LogDebugMessage(exception);
            }
            catch (ArgumentException exception)
            {
                LogDebugMessage(exception);
            }
            catch (NullReferenceException exception)
            {
                LogDebugMessage(exception);
            }
            return messageBuilder;
        }

        private static void AppendExceptionDetails(Exception ex, StringBuilder messageBuilder)
        {
            try
            {
                if (ex.InnerException != null)
                {
                    AppendExceptionDetails(ex.InnerException, messageBuilder);
                }
                if (ex.InnerException == null)
                {
                    messageBuilder.Append(Resource.EX_MESSAGE);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(ex.Message);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);

                    messageBuilder.Append(Resource.EX_TYPE);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(ex.GetType().ToString());
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(Resource.EX_STACKTRACE);
                    messageBuilder.Append(Environment.NewLine);
                }
                messageBuilder.Append(ex.StackTrace);
                messageBuilder.Append(Environment.NewLine);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                LogDebugMessage(exception, messageBuilder.ToString());
            }
            catch (ArgumentException exception)
            {
                LogDebugMessage(exception, messageBuilder.ToString());
            }
            catch (NullReferenceException exception)
            {
                LogDebugMessage(exception, messageBuilder.ToString());
            }
        }

        /// <summary>
        /// Gets an error message to be written in proper format.
        /// </summary>
        /// <param name="category">Log Message Category</param>
        /// <param name="ex">Exception from which information needs to be written to Event Log</param>
        /// <returns>
        /// properly formatted message as below:
        /// Category:
        /// Error Message:
        /// Stack Trace:
        /// </returns>
        private static StringBuilder GetProperMessageFormat
            (LogCategory category, string errorMessage, string stackTrace)
        {
            StringBuilder messageBuilder = null;
            try
            {
                messageBuilder = new StringBuilder(string.Empty);

                messageBuilder.Append(Resource.EX_CATEGORY + SPACE_STRING + category.ToString());
                messageBuilder.Append(Environment.NewLine);
                messageBuilder.Append(Environment.NewLine);

                messageBuilder.Append(Resource.EX_MESSAGE);
                messageBuilder.Append(Environment.NewLine);
                messageBuilder.Append(errorMessage);
                messageBuilder.Append(Environment.NewLine);
                messageBuilder.Append(Environment.NewLine);

                messageBuilder.Append(Resource.EX_STACKTRACE);
                messageBuilder.Append(Environment.NewLine);
                messageBuilder.Append(stackTrace);
                messageBuilder.Append(Environment.NewLine);
                messageBuilder.Append(Environment.NewLine);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogDebugMessage(ex, errorMessage.ToString());
            }
            catch (ArgumentException ex)
            {
                LogDebugMessage(ex, errorMessage.ToString());
            }
            return messageBuilder;
        }

        #endregion

        #region Event Source

        public static void CreateEventSource()
        {
            try
            {
                if (!EventLog.SourceExists(EVENTSOURCE))
                {
                    EventLog.CreateEventSource(EVENTSOURCE, EVENTLOG);
                }
            }
            catch (SecurityException ex)
            {
                LogDebugMessage(ex);
            }
            catch (ArgumentException ex)
            {
                LogDebugMessage(ex);
            }
            catch (InvalidOperationException ex)
            {
                LogDebugMessage(ex);
            }
        }

        #endregion

        #region ILogger Members

        /// <summary>
        /// Writes an information to the Event log with a descriptive message.
        /// </summary>
        /// <param name="information">The contextual information to be logged in Event log</param>
        public void WriteToLog(LogCategory category, string information)
        {
            StringBuilder message = GetProperMessageFormat(category, information);


            CreateInformationEventLogEntry(message);
        }

        /// <summary>
        /// Writes an exception thrown to the Event log. 
        /// </summary>
        /// <param name="ex">Exception to be logged in Event Log</param>
        public void WriteToLog(LogCategory category, Exception ex)
        {
            StringBuilder message = GetProperMessageFormat(category, ex);
            CreateErrorEventLogEntry(message);
        }

        /// <summary>
        /// Writes an exception thrown to the Event log. 
        /// </summary>
        /// <param name="errorMessage">Error message to be logged in Event log</param>
        /// <param name="stackTrace">Error stack trace to be logged in Event log</param>
        public void WriteToLog(LogCategory category, string errorMessage, string stackTrace)
        {
            StringBuilder message = GetProperMessageFormat(category, errorMessage, stackTrace);
            CreateErrorEventLogEntry(message);
        }

        #endregion


        #region Debug Messages

        private static void LogDebugMessage(Exception logException)
        {
            Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "", new string[] { string.Empty, logException.ToString() }));
        }

        private static void LogDebugMessage(Exception logException, string originalException)
        {
            Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "", new string[] { originalException, logException.ToString() }));
        }

        #endregion
        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposeManagedResources)
        {
            if (!this.disposed)
            {
                //Dispose Managed Resource
                if (disposeManagedResources)
                {
                }
                //Dispose Unmanaged Resource
                disposed = true;
            }
        }

        #endregion
    }
    #endregion

    /// <summary>
    /// This class provides an interaction with the eventlog class to create entries into  Windows event log.
    /// </summary>
    public enum LogCategory
    {
        Installer,
        Other
    }
}
