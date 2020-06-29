using System;
using System.Runtime.InteropServices;
using Xunit;

namespace temp_dates_test
{
    public class DateFormattingTests
    {
        [Fact]
        public void OutputsCorrectly()
        {
            var leet = new DateTimeOffset(2020,6,29,13,37,10,new TimeSpan(0,2,0,0));

            var timezoneId = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Europe/Oslo" : "Central European Standard Time";
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var norwegian = TimeZoneInfo.ConvertTime(leet, timeZoneInfo);
            Assert.Equal("2020-06-29 13:37:10+02:00", norwegian.ToString("yyyy-MM-dd HH:mm:sszzz"));
        }
    }
}
