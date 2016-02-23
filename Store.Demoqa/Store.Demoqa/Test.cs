using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Store.Demoqa
{
    /// <summary>
    /// Store.Demoqa Tests
    /// </summary>
    [TestFixture]
    public class Test
    {
        /// <summary>
        /// driver declaration
        /// </summary>
       private IWebDriver driver;

        /// <summary>
        /// Start Firefox browser
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.driver = new FirefoxDriver(); 
        }

        /// <summary>
        /// Open site http://store.demoqa.com/
        /// </summary>
        [Test]
        public void OpenSiteTest()
        {
           this.driver.Navigate().GoToUrl("http://store.demoqa.com/");
        }

        /// <summary>
        /// Close current opened tab in browser
        /// </summary>
        [TearDown]
        public void Close()
        {
            this.driver.Close();
        }
    }
}
