using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeluxeOM.Utils
{
    /// <summary>
    /// Methods for log error, informaion and warning in event log
    /// </summary>
    public class EventLogger : ILogger
    {
        private string _logSource = "DeluxeOrderMgt";
        private string _logName = "";

        /// <summary>
        /// Initialize _logSource and  _logname
        /// </summary>
        /// <param name="logName">consist of controller name</param>
        public EventLogger(string logName)
        {
            _logName = logName;
            if (!System.Diagnostics.EventLog.SourceExists(_logSource))
                this.CreateLog(_logSource);
        }

        /// <summary>
        /// Log information with message in Event log
        /// </summary>
        /// <param name="message">The message to be logged into the event log</param>
        public void Info(string message)
        {
            Log(message, EventLogEntryType.Information);
        }

        /// <summary>
        /// Log warning with message in Event log
        /// </summary>
        /// <param name="message">The message to be logged into the event log</param>
        public void Warn(string message)
        {
            Log(message, EventLogEntryType.Warning);
        }

        /// <summary>
        /// Log Error with message in Event log
        /// </summary>
        /// <param name="message">The message to be logged into the event log</param>
        public void Error(string message)
        {
            Log(message, EventLogEntryType.Error);
        }


        /// <summary>
        /// Create log source in Event log
        /// </summary>
        /// <param name="strLogSource">DeluxeOrderMgt</param>
        /// <returns></returns>
        private bool CreateLog(string strLogSource)
        {
            bool Result = false;

            try
            {
                System.Diagnostics.EventLog.CreateEventSource(_logSource, _logName );
                System.Diagnostics.EventLog SQLEventLog = new System.Diagnostics.EventLog();

                SQLEventLog.Source = strLogSource;
                SQLEventLog.Log = _logName;

                SQLEventLog.Source = _logName;
                SQLEventLog.WriteEntry("The " + strLogSource + " was successfully initialize component.", EventLogEntryType.Information);

                Result = true;
            }
            catch
            {
                Result = false;
            }
            return Result;
        }


        /// <summary>
        /// Create log in Event log
        /// </summary>
        /// <param name="message">The message to be logged into the event log</param>
        /// <param name="logType">contains information, warning and error</param>
        private void Log(string message, EventLogEntryType logType)
        {
            System.Diagnostics.EventLog SQLEventLog = new System.Diagnostics.EventLog();

            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(_logSource))
                    this.CreateLog(_logSource);


                SQLEventLog.Source = _logSource;
                SQLEventLog.WriteEntry(Convert.ToString(_logName) + ": " + Convert.ToString(message), logType);
            }
            catch (Exception ex)
            {
                SQLEventLog.Source = _logSource;
                SQLEventLog.WriteEntry(Convert.ToString("INFORMATION: ")
                                      + Convert.ToString(ex.Message),
                                        EventLogEntryType.Error);
            }
            finally
            {
                SQLEventLog.Dispose();
                SQLEventLog = null;
            }
        }
    }
}
