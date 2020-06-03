using System;
using Xunit;
using System.Collections.Generic;

namespace LogParser.Test
{
    public class UnitTest1
    {
        [Fact]
        ////    getRequiredLogs return matching loglevel logs 
        public void getRequiredLogs_success()
        {
            ParseLogFile parseLogFile = new ParseLogFile();
            List<string> log = new List<string>();

            log.Add("03/20 test INFO");
            log.Add("04/20,test,error");
            log.Add("05/20,test,error");
            log.Add("05/20,test,WARN");
            log.Add("05/20,test,trace");

            List<string> level = new List<string>();
            level.Add("info");
            level.Add("warn");

            List<string> expect = parseLogFile.getRequiredLogs(log, level);
            Assert.Equal(2, expect.Count);
            Assert.Equal(log[0], expect[0]);
        }

        [Fact]
        ////    getRequiredLogs return empty list when logsList is empty, 
        public void getRequiredLogs_failure()
        {
            ParseLogFile parseLogFile = new ParseLogFile();

            List<string> log = new List<string>();
            List<string> level = new List<string>();

            List<string> expect = parseLogFile.getRequiredLogs(log, level);
            Assert.Equal(0, expect.Count);
        }


        [Fact]
        /// should return third space seperated value.
        public void getLogLevel_success()
        {
            ProcessLogcontent processLogcontent = new ProcessLogcontent();
            var level = processLogcontent.getLogLevel("date time error");
            Assert.Equal("error", level);
        }

        [Fact]
        /// should return empty string if no third space sepearted value
        public void getLogLevel_failure()
        {
            ProcessLogcontent processLogcontent = new ProcessLogcontent();
            var level = processLogcontent.getLogLevel("date time");
            Assert.Equal("", level);
        }


        [Fact]
        // getDate should return date in required format by reading first comma seperated value frm given string.
        public void getDate_success()
        {
            ProcessLogcontent processLogcontent = new ProcessLogcontent();
            string logLine = "05/22 01:02:33 error :...this is first log";
            var expected_Date = processLogcontent.getdate(logLine);
            //    Assert.Equal("22 May 2020",expected_Date);
        }

        [Fact]
        // getDate should return default date "3 june 2020" if not date syntax found in given log.
        public void getDate_failure()
        {
            ProcessLogcontent processLogcontent = new ProcessLogcontent();
            string logLine = "05:06 error :...this is first log";
            var expected_Date = processLogcontent.getdate(logLine);
            Assert.Equal("3 June 2020", expected_Date);
        }

        [Fact]
        // getTime should return time in expected format from given numeric date in given logLine. 
        public void getTime_success()
        {
            ProcessLogcontent processLogcontent = new ProcessLogcontent();
            string logLine = "05/22 01:02:33 error :...this is first log";
            var expected_time = processLogcontent.getTime(logLine);
            Assert.Equal("1:02 AM", expected_time);

        }
        [Fact]
        // getTime should return default time "12:00 AM "in when not date string is available in given logLine. 
        public void getTime_failure()
        {
            ProcessLogcontent processLogcontent = new ProcessLogcontent();
            string logLine = "05/22 01/02/33 error :...this is first log";
            var expected_time = processLogcontent.getTime(logLine);
            Assert.Equal("12:00 AM", expected_time);
        }

        [Fact]
        // return description string from given logline
        public void getDescription_success()
        {
            ProcessLogcontent processLogcontent = new ProcessLogcontent();
            string logLine = "05/22 01/02/33 error :...this is first log";
            string expected = processLogcontent.getDescription(logLine);
            Assert.Equal(":...this is first log", expected);

        }

        [Fact]
        /// return empty string if no description is present in logline.
        public void getDescription_failure()
        {
            ProcessLogcontent processLogcontent = new ProcessLogcontent();
            string logLine = "05/22 01/02/33 error :";
            string expected = processLogcontent.getDescription(logLine);
            Assert.Equal(null, expected);

        }
        [Fact]
        //get list of object depending on given list of logline.
        public void getCsvCompliantContent()
        {
            List<ProcessLogcontent> processLogcontents = new List<ProcessLogcontent>();
            ProcessLogcontent processLogcontent = new ProcessLogcontent();

            List<string> logline = new List<string>();
            logline.Add("05/22 01:02:33 error :...this is first log");
            logline.Add("05/23 01:02:33 error :...this is second log");
            logline.Add("05/24 01:02:33 error :...this is third log");

            processLogcontents = processLogcontent.getCsvCompliantContent(logline);
            Assert.Equal(3, processLogcontents.Count);


        }

        [Fact]
        //get empty list if logline passed is empty.
        public void getCsvCompliantContent_emptyLogLine()
        {
            List<ProcessLogcontent> processLogcontents = new List<ProcessLogcontent>();
            ProcessLogcontent processLogcontent = new ProcessLogcontent();

            List<string> logline = new List<string>();

            processLogcontents = processLogcontent.getCsvCompliantContent(logline);
            Assert.Equal(0, processLogcontents.Count);

        }

    }
}
