using System;
using System.IO;
using System.Collections.Generic;
namespace Leave_Tracker
{
    public class Validations
    {
        public static Boolean isNecessaryInput(string csvFilePath)
        {
            bool isFile = File.Exists(csvFilePath);
            if (!isFile)
                return false;
            return true;
        }

        public static Boolean isFilePresent(string filePath)
        {
            if (File.Exists(filePath))
                return true;
            return false;
        }

        public Boolean isManager(int manId, List<int> ManIDs)
        {
            if (ManIDs.IndexOf(manId) == -1)
                return false;
            return true;
        }


    }

    public class CsvInfo
    {
        public string employeeInfoCsv;
        public string LeaveRecordFile;


        public CsvInfo GetCsvInfo()
        {
            // Console.WriteLine("please Enter the paths for Employee info csv file paths");
            // this.employeeInfoCsv = Console.ReadLine();


            // Console.WriteLine("please Enter the paths for leave Record Csv file paths");
            // this.LeaveRecordFile = Console.ReadLine();


            this.employeeInfoCsv = @"C:\Users\avinasht\Downloads\employees.csv";
            this.LeaveRecordFile = @"C:\Users\avinasht\Downloads\leaves.csv";
            return this;
        }


    }

}