using System;
using System.IO;
using System.Collections.Generic; 

namespace LogParser
{
    public class ParseLogFile
    {
        public List<string> parseFileToStringList(string logFileDir)
        {
            string[] filePaths = Directory.GetFiles(logFileDir, "*.log");
            List<string> storeAllLogs = new List<string>();
            string line;
            foreach (var file in filePaths)
            {
                try
                {   // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader(file))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                                storeAllLogs.Add(line);
                        }
                    }
                    Console.WriteLine($"{file} parse successfull  !!!");
                }
                catch (IOException e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }

            return storeAllLogs;
        }

        public List<string> getRequiredLogs(List<string> allLogs,List<string> logLevel){
            List<string> requiredLogs=new List<string>();

            foreach(var log in allLogs){
                foreach(var level in logLevel){
                    if(log.Contains(level.ToUpper()))
                    requiredLogs.Add(log);
                }
            }
            return requiredLogs;
        }

    }
}