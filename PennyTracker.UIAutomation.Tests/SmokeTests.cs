using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace PennyTracker.UIAutomation.Tests
{
    [TestClass]
    public class SmokeTests
    {
        IWebDriver webDriver = new ChromeDriver();
        [TestInitialize]
        public void Initialize()
        {
            webDriver.Navigate().GoToUrl("https://localhost:5001/");
        }

        [TestMethod]
        public void Test()
        {

        }
     }
}
