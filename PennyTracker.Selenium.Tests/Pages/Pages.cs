using SeleniumExtras.PageObjects;

namespace PennyTracker.Selenium.Tests.Pages
{
    public static class Pages
    {
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(Browser.Driver, page);
            return page;
        }

        public static HomePage Home
        {
            get { return GetPage<HomePage>(); }
        }

        public static ChartPage Chart
        {
            get { return GetPage<ChartPage>(); }
        }

        public static NavigationPane Navigation
        {
            get { return GetPage<NavigationPane>(); }
        }
    }
}
