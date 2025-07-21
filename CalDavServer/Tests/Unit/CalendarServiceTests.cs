using Xunit;
using CalDavServer.Services;
using System;

namespace CalDavServer.Tests.Unit
{
    public class CalendarServiceTests
    {
        [Fact]
        public void GetAll_ReturnsEmptyList()
        {
            var service = new CalendarService();
            var result = service.GetAll();
            Assert.Empty(result);
        }
    }
}