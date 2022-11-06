using System;
using System.Diagnostics;
using System.IO;

namespace SigmaHubApi.Modal
{
    public class Logs
    {
        public const string constant = "cSharp";
        public static double CurrentDistrictID;
        public static void Writelog(string Message)
        {
            string logFile = AppDomain.CurrentDomain.BaseDirectory + @"Logs\\" + DateTime.Now.Date.ToString("ddMMMyyyy") + ".txt";
            string logTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            Message = logTime + "  => " + Message;
            StreamWriter sw;
            FileStream fs = null;
            try
            {
                if ((!Directory.Exists("logs")))
                    Directory.CreateDirectory("Logs");

                if ((!File.Exists(logFile)))
                {
                    fs = File.Create(logFile);
                    fs.Dispose();
                    fs = null;
                    sw = File.AppendText(logFile);
                    sw.WriteLine(Message);
                    sw.Close();
                }
                else
                {
                    sw = File.AppendText(logFile);
                    sw.WriteLine(Message);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                // Create an EventLog instance and assign its source. 
                EventLog myLog = new EventLog();
                myLog.Source = "ELogger";
                // Write an informational entry to the event log.    
                myLog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
