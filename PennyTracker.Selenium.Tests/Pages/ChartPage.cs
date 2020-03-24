﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PennyTracker.Selenium.Tests.Pages
{
    public class ChartPage
    {
        [FindsBy(How = How.XPath, Using = "//h3")]
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
