using Xunit;
using CalDavServer.Services;
using System;

namespace CalDavServer.Tests.Unit
{
    public class EventServiceTests
    {
        [Fact]
        public void GetAll_ReturnsEmptyList()
        {
            var service = new EventService();
            var result = service.GetAll();
            Assert.Empty(result);
        }
    }
}