using System;
using System.IO;
using System.Collections.Generic;

namespace LogParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Start of the project
            Validations validations = new Validations();

            if (!validations.checkForArgumentCount(args))
                return;

            CommandInput commandInput = new CommandInput();

            commandInput.getCommandinput(args);

            if (!CommandInput.CheckLogDir(commandInput.logFilePath))
                return;

            ParseLogFile parseLogFile = new ParseLogFile();

            List<string> allLogs = parseLogFile.parseFileToStringList(commandInput.logFilePath);
            List<string> requiredLogs = parseLogFile.getRequiredLogs(allLogs, commandInput.loglevel);

            ProcessLogcontent processLogcontent = new ProcessLogcontent();
            var csvContent = processLogcontent.getCsvCompliantContent(requiredLogs);

            CsvGeneration csvGeneration = new CsvGeneration();
            csvGeneration.createCsvFile(csvContent, commandInput.csvFilePath);

        }
    }
}
