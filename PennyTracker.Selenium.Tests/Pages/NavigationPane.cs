using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PennyTracker.Selenium.Tests.Pages
{
    public class NavigationPane
    {
        [FindsBy(How = How.PartialLinkText, Using = "Home")]
        private IWebElement homeLink;

        [FindsBy(How = How.PartialLinkText, Using = "Chart")]
        private IWebElement chartLink;

        public void GoToHome()
        {
            homeLink.Click();
        }

        public void GoToChart()
        {
            chartLink.Click();
        }
    }
}
