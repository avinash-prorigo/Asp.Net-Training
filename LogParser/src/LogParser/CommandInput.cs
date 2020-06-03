using System;
using System.IO;
using System.Collections.Generic;

namespace LogParser
{
    public class CommandInput
    {
        public const string inputDir = "--log-dir";
        public const string ouputDir = "--csv-dir";
        public const string LogLevel = "--log-level";

        public List<string> loglevel;
        public string logFilePath;
        public string csvFilePath;

        public CommandInput()
        {
            this.loglevel = new List<string>();
        }


        public CommandInput getCommandinput(string[] args)
        {
            Boolean isLogDir = false;
            Boolean isCsvDir = false;

            //Store Command Inputs
            for (int index = 0; index < args.Length; index++)
            {
                switch (args[index])
                {
                    case CommandInput.inputDir:
                        if (!isLogDir)
                        {
                            this.logFilePath = args[++index];
                            isLogDir = true;
                        }
                        else{
                            ++index;
                        }
                        break;

                    case CommandInput.ouputDir:
                        if (!isCsvDir)
                        {
                            this.csvFilePath = args[++index];
                            isCsvDir = true;
                        }
                        else{
                            ++index;
                        }
                        break;

                    case CommandInput.LogLevel:
                        this.loglevel.Add(args[++index]);
                        break;

                    case "--help":
                    case "-h":
                        CommandInput.showUsage();
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input Paramter Passed");
                        CommandInput.showUsage();
                        System.Environment.Exit(0);
                        break;
                }

            }
            return this;
        }

        public static Boolean CheckLogDir(string logDirPath)
        {
            if (!Directory.Exists(logDirPath))
            {
                Console.WriteLine("Entered Wrong Log Directory Path");
                CommandInput.showUsage();
                return false;
            }
            return true;
        }
        public static void showUsage()
        {

            Console.WriteLine("\n Show Usage:\n");
            Console.WriteLine("Usage: logParser --log-dir <dir> --log-level <level> --csv <out>");
            Console.WriteLine("\t --log-dir   Directory to parse recursively for .log files");
            Console.WriteLine("\t --csv-dir   Out file-path (absolute/relative)");
        }

    }
    public class Validations
    {
        public Boolean checkForArgumentCount(string[] args)
        {
            if (args.Length % 2 == 1 || args.Length < 6)
            {
                Console.WriteLine("Please provide the correct Input arguments");
                CommandInput.showUsage();
                return false;
            }
            return true;
        }

        public Boolean getInputDirectoryStatus(CommandInput commandInput)
        {

            if (commandInput.logFilePath == null || commandInput.csvFilePath == null ||
                            !CommandInput.CheckLogDir(commandInput.logFilePath))
            {
                Console.WriteLine("Please Enter necessary and correct directory path");
                return false;
            }
            return true;

        }


    }
}