using NUnit.Framework;
using Utils;

namespace Tests
{
    public class HelperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetSettingsTests()
        {
            var settings = Helpers.GetMessagingSettings();
            Assert.IsNotNull(settings);
        }
    }
}