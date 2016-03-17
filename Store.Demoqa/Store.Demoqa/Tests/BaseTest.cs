using NUnit.Framework;
using OpenQA.Selenium;
using Store.Demoqa.Helpers;
using Store.Demoqa.Pages;
using System;

namespace Store.Demoqa.Tests
{
    [TestFixture]
    public class BaseTest
    {
        public HomePage homePage;
        public static PageRepository repository;
        private IWebDriver driver;

        /// <summary>
        /// Sets up creation of new HomePage before each test in suite
        /// </summary>
        [SetUp]
        public virtual void Init()
        {
            Browser browser = new Browser();
            driver = browser.GetDriver();
            driver.Navigate().GoToUrl(Config.GetSite());
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.TIMETOWAIT));
            repository = new PageRepository(driver);
            homePage = repository.Get<HomePage>();
        }

        /// <summary>
        /// Closes the browser
        /// </summary>
        [TearDown]
        public virtual void CloseBrowser()
        {
            driver.Dispose();
        }
    }
}
