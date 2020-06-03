using System;
using System.IO;
using System.Collections.Generic;
using Xunit;

namespace LogParser.Test
{
    public class UnitTest2
    {
        [Fact]
        public void createCsvFile()
        {
            List<ProcessLogcontent> processLogcontents = new List<ProcessLogcontent>();
            ProcessLogcontent processLogcontent = new ProcessLogcontent();

            List<string> logline = new List<string>();
            logline.Add("05/22 01:02:33 error :...this is first log");
            logline.Add("05/23 01:02:33 error :...this is second log");
            logline.Add("05/24 01:02:33 error :...this is third log");

            processLogcontents = processLogcontent.getCsvCompliantContent(logline);

            CsvGeneration csvGeneration=new CsvGeneration();
            csvGeneration.createCsvFile(processLogcontents,"test.csv");

            Assert.True(File.Exists("test.csv"));
            File.Delete("test.csv");
        }
        
    }
}