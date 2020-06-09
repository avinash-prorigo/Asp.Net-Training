using System;
using System.IO;
using System.Collections.Generic;

namespace Leave_Tracker
{
    public class Leave
    {
        int ID;
        string Creator;
        string Manager;
        string Title;
        string Description;
        string StartDate;
        string EndDate;
        List<string> StatusList;
        string Status;
        public Leave()
        {

            this.StatusList = new List<string>() { "Pending", "Approved", "Rejected" };
        }

        public Leave createLeave(int empID, string empName, string manName)
        {
            this.ID = empID;
            this.Creator = empName;
            this.Manager = manName;
            Console.WriteLine("Enter the Title for Leave:");
            this.Title = Console.ReadLine();
            Console.WriteLine("Enter the Description for Leave:");
            this.Description = Console.ReadLine();
            Console.WriteLine("Enter the Start Date of leave:");
            this.StartDate = Console.ReadLine();
            Console.WriteLine("Enter the End Date of Leave:");
            this.EndDate = Console.ReadLine();
            this.Status = this.StatusList[0];

            return this;

        }

        public void writeLeaveTOcsv(string LeaveRecordFile)
        {
            string delimiter = ",";
            string appendLeave;

            if (!File.Exists(LeaveRecordFile))
            {
                string createText = "ID" + delimiter + "Creator" + delimiter + "Manager" + delimiter + "Title" + delimiter
                + "Description" + delimiter + "Start-Date" + delimiter + "End-Date" + delimiter + "Status" + Environment.NewLine;
                File.WriteAllText(LeaveRecordFile, createText);
            }

            appendLeave = this.ID + delimiter + this.Creator + delimiter + this.Manager + delimiter
            + this.Title + delimiter + this.Description + delimiter + this.StartDate + delimiter
            + this.EndDate + delimiter + this.Status + Environment.NewLine;

            File.AppendAllText(LeaveRecordFile, appendLeave);
        }

        public List<Leave> getLeaveRecord(string LeaveRecordFile)
        {
            List<Leave> leaves = new List<Leave>();
            Boolean SkipFirstRow = false;
            Leave leave = new Leave();

            using (var reader = new StreamReader(LeaveRecordFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (SkipFirstRow && line != null && line.Contains(","))
                    {
                        var values = line.Split(',');
                        leave.ID = Int32.Parse(values[0]);
                        leave.Creator = values[1];
                        leave.Manager = values[2];
                        leave.Title = values[3];
                        leave.Description = values[4];
                        leave.StartDate = values[5];
                        leave.EndDate = values[6];
                        leave.Status = values[7];
                    }
                    if (SkipFirstRow)
                        leaves.Add(leave);
                    SkipFirstRow = true;
                }
            }

            return leaves;
        }

        public void PrintListOfLeavesById(int empID, List<Leave> leaveRecords)
        {
            Boolean isLeavePresent = false;
            foreach (var leave in leaveRecords)
            {
                if (leave.ID == empID)
                {
                    this.PrintLeave(leave);
                    // Console.WriteLine($"{leave.ID},{leave.Creator},{leave.Manager},{leave.Title},{leave.Description},{leave.StartDate},{leave.EndDate},{leave.Status}");
                    isLeavePresent = true;
                }
            }
            if (!isLeavePresent)
                Console.WriteLine($"No leaves record for employee No-{empID} ");

        }

        // public List<Leave> getLeaveByManagerName(string manName, string LeaveRecordFile)
        // {
        //     var requiredLeave = new List<Leave>();
        //     var leaveRecord = this.getLeaveRecord(LeaveRecordFile);

        //     foreach (var leave in leaveRecord)
        //     {
        //         if (leave.Manager == manName)
        //             requiredLeave.Add(leave);
        //     }
        //     return requiredLeave;
        // }


        public void updateLeaveStatus(string manName, string LeaveRecordFile)
        {
            Boolean isFirstWrite = false;
            var leaveRecord = this.getLeaveRecord(LeaveRecordFile);

            // var required_Leaves = this.getLeaveByManagerName(manName, LeaveRecordFile);

            if (leaveRecord.Count > 0)
            {
                Console.WriteLine("Enter new status value");
                var status = Console.ReadLine();

                if (this.StatusList.Contains(status))
                {
                    foreach (var leave in leaveRecord)
                    {
                        if (leave.Manager == manName)
                            leave.Status = status;
                        if (!isFirstWrite && File.Exists(LeaveRecordFile))
                        {
                            File.Delete(LeaveRecordFile);
                            isFirstWrite=true;
                        }
                        leave.writeLeaveTOcsv(LeaveRecordFile);
                    }
                    Console.WriteLine("Status Chnaged Successfully !!\n\n");
                }
                else
                {
                    Console.WriteLine("Entered status value is Invalid !! \n\n");
                }
            }
            else
            {
                Console.WriteLine("No leaves assigned to this manager\n\n");
            }
        }


        public void showLeaveByTitle(List<Leave> leaveRecord)
        {
            Console.WriteLine("Enter the Title to search :");
            Boolean isLeaveRecord = false;
            var Title = Console.ReadLine();
            foreach (var Leave in leaveRecord)
            {

                if (Leave.Title == Title)
                {
                    isLeaveRecord = true;
                    this.PrintLeave(Leave);
                }
            }
            if (!isLeaveRecord)
                Console.WriteLine("No leave Record found by given title !!\n\n");
        }

        public void showLeaveByStatus(List<Leave> leaveRecord)
        {
            Console.WriteLine("Enter the Status to search :");

            Boolean isLeaveRecord = false;
            var status = Console.ReadLine();
            foreach (var Leave in leaveRecord)
            {

                if (Leave.Status == status)
                {
                    isLeaveRecord = true;
                    this.PrintLeave(Leave);
                }
            }
            if (!isLeaveRecord)
                Console.WriteLine("No leave Record found by given status !!\n\n");
        }

        public void PrintLeave(Leave leave)
        {
            Console.WriteLine($"{leave.ID},{leave.Creator},{leave.Manager},{leave.Title},{leave.Description},{leave.StartDate},{leave.EndDate},{leave.Status}");

        }
    }
}