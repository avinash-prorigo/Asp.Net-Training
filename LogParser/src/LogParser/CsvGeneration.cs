using System.IO;
using System;
using System.Collections.Generic;

namespace LogParser
{
    public class CsvGeneration
    {
        public void createCsvFile(List<ProcessLogcontent> logcontents, string csvFile)
        {
            string delimiter = ", ";
            string appendLogs;
            int counter = 1;

            if (File.Exists(csvFile))
                File.Delete(csvFile);

            if (!File.Exists(csvFile))
            {
                string createText = "No" + delimiter + "Level" + delimiter + "Date" + delimiter + "Time" + delimiter + "Text" + Environment.NewLine;
                File.WriteAllText(csvFile, createText);
            }
            foreach (ProcessLogcontent processLogcontent in logcontents)
            {
                appendLogs = counter++ + delimiter + processLogcontent.Level + delimiter + processLogcontent.Date + delimiter + processLogcontent.Time +
                delimiter + "\"" + processLogcontent.Description + "\"" + Environment.NewLine;
                File.AppendAllText(csvFile, appendLogs);
            }

            Console.WriteLine($"{csvFile} file is created successFully");

        }
    }
}