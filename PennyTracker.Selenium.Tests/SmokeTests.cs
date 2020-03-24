using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PennyTracker.Selenium.Tests.Extentions;
using System.Threading;

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

        //IWebDriver webDriver = new ChromeDriver();

        //[TestInitialize]
        //public void Initialize()
        //{
        //    webDriver.Navigate().GoToUrl("https://localhost:5001/");
        //    webDriver.Manage().Window.Maximize();
        //}

        //[TestMethod]
        //public void TestMethod1()
        //{
        //    var addButton = webDriver.FindElement(By.ClassName("ui-button-text"));
        //    addButton.Click();


        //    var cancelButton = webDriver.FindElement(By.XPath("//span[text() = 'Cancel']"), 1);
        //    cancelButton.Click();
        //}

        [TestMethod]
        public void CanGoToChartPage()
        {
            Pages.Pages.Chart.Goto();
            Assert.IsTrue(Pages.Pages.Chart.HasHeadLine("Reports"));
        }
    }
}
