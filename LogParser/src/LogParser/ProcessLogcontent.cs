using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace LogParser
{
    public class ProcessLogcontent
    {
        public string Level;
        public string Date;
        public string Time;
        public string Description;

        public string getdate(string logLine)
        {
            string DateField = Regex.Match(logLine, @"(?<=^(\S+\s){0})\S+").ToString();
            DateTime dt = DateTime.Parse(DateField);
            return ($"{dt.Day} {dt.ToString("MMMM")} {dt.Year}").ToString();
        }

        public string getTime(string logLine)
        {
            string TimeField = Regex.Match(logLine, @"(?<=^(\S+\s){1})\S+").ToString();
            DateTime tm = DateTime.Parse(TimeField);
            return tm.ToShortTimeString();
        }
        public string getLogLevel(string logLine)
        {
            string logLevel = Regex.Match(logLine, @"(?<=^(\S+\s){2})\S+").ToString();
            if (logLevel.Contains(":")){
                string[] logLevelPart = logLevel.Split(":");
                return logLevelPart[0];
            }
            return logLevel;
        }
        public string getDescription(string logLine)
        {
            string[] fields = logLine.Split(":.");
            if (fields.Length >= 2)
                return ":." + fields[1].Trim();
            return null;
        }

        public List<ProcessLogcontent> getCsvCompliantContent(List<string> logLines)
        {
            List<ProcessLogcontent> CsvContents = new List<ProcessLogcontent>();

            foreach (var line in logLines)
            {
                try
                {
                    var csvContent = new ProcessLogcontent();
                    csvContent.Level = csvContent.getLogLevel(line);
                    csvContent.Date = csvContent.getdate(line);
                    csvContent.Time = csvContent.getTime(line);
                    csvContent.Description = csvContent.getDescription(line);

                    CsvContents.Add(csvContent);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }
            }

            return CsvContents;
        }
    }
}