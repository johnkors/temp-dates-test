using System;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;

namespace temp_dates_test
{
    public class DateFormattingTests
    {
        private readonly ITestOutputHelper _helper;

        public DateFormattingTests(ITestOutputHelper helper)
        {
            _helper = helper;
        }
        
        [Fact]
        public void OutputsCorrectly_FromDateTimeOffset1()
        {
            var leet = new DateTimeOffset(2020,6,29,13,37,10,new TimeSpan(0,2,0,0));

            var timezoneId = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Europe/Oslo" : "Central European Standard Time";
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            
            var norwegian = TimeZoneInfo.ConvertTime(leet, timeZoneInfo);
            Assert.Equal("2020-06-29 13:37:10+02:00", norwegian.ToString("yyyy-MM-dd HH:mm:sszzz"));
        }
        
        [Fact]
        public void OutputsCorrectly_FromDateTimeOffset2()
        {
            var timezoneId = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Europe/Oslo" : "Central European Standard Time";
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            
            var date = new DateTime(2020,6,29, 13,37,10);
            var offset = timeZoneInfo.GetUtcOffset(date);
            var dateWithOffset = new DateTimeOffset(date,offset);

            Assert.Contains("2020-06-29 13:37:10+02:00", dateWithOffset.ToString("yyyy-MM-dd HH:mm:sszzz"));
        }
        
        [Fact]
        public void OutputsCorrectly_FromDateTimeDate()
        {
            var timezoneId = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Europe/Oslo" : "Central European Standard Time";
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            
            var date = new DateTime(2020,6,29, 13,37,10);
            var converted = TimeZoneInfo.ConvertTime(date, timeZoneInfo);

            Assert.Equal("2020-06-29 13:37:10+02:00", converted.ToString("yyyy-MM-dd HH:mm:sszzz"));
        }
    }
}
