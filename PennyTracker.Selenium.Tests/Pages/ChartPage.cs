using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PennyTracker.Selenium.Tests.Pages
{
    public class ChartPage
    {
        [FindsBy(How = How.XPath, Using = "//a[@href = '/report']")]
        private IWebElement pageHeadline;

        public void Goto()
        {
            Pages.Navigation.GoToChart();
        }

        public bool HasHeadLine(string headline)
        {
            return headline.Equals(pageHeadline.Text);
        }
    }
}
