using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PennyTracker.Selenium.Tests.Extentions
{
    public static class WaitExtention
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, TimeSpan? timeout = null)
        {
            if (timeout.HasValue)
            {
                var wait = new WebDriverWait(driver, timeout.Value);

                return wait.Until(drv => drv.FindElement(by));
            }

            return driver.FindElement(by);
        }
    }
}
