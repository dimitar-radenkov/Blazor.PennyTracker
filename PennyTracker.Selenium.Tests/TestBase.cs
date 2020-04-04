using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PennyTracker.Selenium.Tests
{
    [TestClass]
    public class TestBase
    {
        [TestInitialize]
        public static void Initialize()
        {
            Browser.Initialize();
        }
    }
}
