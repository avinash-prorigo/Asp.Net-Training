using System;
using System.IO;
using System.Collections.Generic;

namespace Leave_Tracker
{
    public class EmployeeManagerRelations
    {
        public Dictionary<int, int> empID_manId = new Dictionary<int, int>();
        public Dictionary<int, string> empID_empName = new Dictionary<int, string>();


        public EmployeeManagerRelations parseEmployeeInfoCsv(string empCSVPath)
        {
            int empId, manId;
            string empName;
            Boolean SkipFirstRow = false;
            EmployeeManagerRelations employeeManagerRelation = new EmployeeManagerRelations();

            using (var reader = new StreamReader(empCSVPath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (SkipFirstRow && line != null && line.Length > 5/*Contains(",")*/)
                    {

                        var values = line.Split(',');

                        empId = Int32.Parse(values[0]);
                        empName = values[1].Trim();
                        if (values[2] != "")
                        {
                            manId = Int32.Parse(values[2]);
                        }
                        else
                            manId = 0;
                        employeeManagerRelation.empID_manId.Add(empId, manId);
                        employeeManagerRelation.empID_empName.Add(empId, empName);

                    }
                    SkipFirstRow = true;
                }
            }
            return employeeManagerRelation;
        }

    }
}