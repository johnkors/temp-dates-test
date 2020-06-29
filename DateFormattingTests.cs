using System;
using System.Globalization;
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
        public void OutputsCorrectly_FromDateTimeOffset_ConvertedWithConvertTime()
        {
            var leet = new DateTimeOffset(2020,6,29,13,37,10,new TimeSpan(0,2,0,0));
            var norwegian = TimeZoneInfo.ConvertTime(leet, GetNorwegianTimeZoneInfo());
            
            Assert.Equal("2020-06-29 13:37:10+02:00", norwegian.ToString("yyyy-MM-dd HH:mm:sszzz"));
        }

        [Fact]
        public void OutputsCorrectly_FromDateTimeOffset_Directly()
        {
            var date = new DateTime(2020,6,29, 13,37,10);
            var offset = GetNorwegianTimeZoneInfo().GetUtcOffset(date);
            var dateWithOffset = new DateTimeOffset(date,offset);

            Assert.Contains("2020-06-29 13:37:10+02:00", dateWithOffset.ToString("yyyy-MM-dd HH:mm:sszzz"));
        }

        [Fact]
        public void OutputsCorrectly_FromDateTime_ConvertedWithConvertTime()
        {
            var date = new DateTime(2020,6,29, 13,37,10);
            var converted = TimeZoneInfo.ConvertTime(date, GetNorwegianTimeZoneInfo());
            Assert.Equal("ivl", System.Threading.Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName);
            Assert.Equal("2020-06-29 15:37:10+00:00", converted.ToString("yyyy-MM-dd HH:mm:sszzz"));
        }

        private static TimeZoneInfo GetNorwegianTimeZoneInfo()
        {
            var timezoneId = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "Europe/Oslo"
                : "Central European Standard Time";
            return TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
        }
    }
}
