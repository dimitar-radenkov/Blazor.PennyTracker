using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PennyTracker.Selenium.Tests
{
    [TestClass]
    public class SmokeTests
    {
        [TestInitialize]
        public void Initialize()
        {
            Browser.Initialize();
        }

        [TestMethod]
        public void CanGoToChartPage()
        {
            Pages.Pages.Chart.Goto();
            Assert.IsTrue(Pages.Pages.Chart.HasHeadLine("Reports"));
        }
    }
}
