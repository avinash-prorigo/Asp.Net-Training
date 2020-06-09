using System;
using System.Collections.Generic;

namespace Leave_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start of the program");

            CsvInfo csvInfo = new CsvInfo();
            csvInfo.GetCsvInfo();

            Validations validations = new Validations();
            Validations.isNecessaryInput(csvInfo.employeeInfoCsv);

            EmployeeManagerRelations employeeManagerRelations = new EmployeeManagerRelations();
            EmployeeManagerRelations relations = employeeManagerRelations.parseEmployeeInfoCsv(csvInfo.employeeInfoCsv);

            List<int> List_empId = new List<int>(relations.empID_manId.Keys);
            List<int> List_manId = new List<int>(relations.empID_manId.Values);
            List<string> List_empName = new List<string>(relations.empID_empName.Values);


        // Console.WriteLine($"{iter}==>{record.empID_empName[iter]}");
        start:
            Console.WriteLine("Enter the employee ID");
            int emp_ID = Int32.Parse(Console.ReadLine());
            // int emp_ID = 105;

            while (true)
            {
                Console.WriteLine("\n*******************START*********************");
                Console.WriteLine("\n\t 1.Assign to");
                Console.WriteLine("\t 2.List My Leaves");
                Console.WriteLine("\t 3.Update Leaves");
                Console.WriteLine("\t 4.Search Leave By");
                Console.WriteLine("\t 5.Change User");
                Console.WriteLine("\t 6.Exit");


                Console.WriteLine("\n\tEnter the user choice:");
                int choice = Int32.Parse(Console.ReadLine());
                // int choice=1;
                Leave leave = new Leave();
                // leave.LeaveRecordFile=csvInfo.LeaveRecordFile;
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Please enter the manager Id ");
                        int manID = Int32.Parse(Console.ReadLine());
                        if (validations.isManager(manID, List_manId) && relations.empID_manId[emp_ID] == manID)
                        {
                            leave.createLeave(emp_ID, relations.empID_empName[emp_ID], relations.empID_empName[manID]);
                            leave.writeLeaveTOcsv(csvInfo.LeaveRecordFile);
                            Console.WriteLine($"Leave added successfully to file {csvInfo.LeaveRecordFile}");
                        }
                        else
                        {
                            Console.WriteLine("Given ID is not current employee manager or may Not manager\n");
                        }
                        break;

                    case 2:

                        var leaveRecords = leave.getLeaveRecord(csvInfo.LeaveRecordFile);
                        leave.PrintListOfLeavesById(emp_ID, leaveRecords);
                        break;
                    case 3:
                        if (validations.isManager(emp_ID, List_manId))
                        {
                            leave.updateLeaveStatus(relations.empID_empName[emp_ID], csvInfo.LeaveRecordFile);
                        }
                        else
                        {
                            Console.WriteLine("Only manager is able to update leave !!");
                        }
                        break;

                    case 4:
                        Console.WriteLine("Search Leave by :\n\t 1.Title\n\t 2.status");
                        var searchChoice = Int32.Parse(Console.ReadLine());
                        var leaveRecord = leave.getLeaveRecord(csvInfo.LeaveRecordFile);
                        if (searchChoice == 1)
                        {
                            leave.showLeaveByTitle(leaveRecord);
                        }
                        else if (searchChoice == 2)
                        {
                            leave.showLeaveByStatus(leaveRecord);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Choice !!");
                        }
                        break;

                    case 5:
                        goto start;
                    case 6:
                        return;
                    default:

                        break;

                }

            }

        }

    }
}
