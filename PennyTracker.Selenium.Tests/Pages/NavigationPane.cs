using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PennyTracker.Selenium.Tests.Pages
{
    public class NavigationPane
    {
        [FindsBy(How = How.PartialLinkText, Using = "Home")]
        private IWebElement homeLink;

        [FindsBy(How = How.PartialLinkText, Using = "Reports")]
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
