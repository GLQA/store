using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace Store.Demoqa
{
    /// <summary>
    /// Store.Demoqa Tests
    /// </summary>
    [TestFixture]
    public class TestSuite
    {
        private const string SITEURL = "http://store.demoqa.com/";
        /// <summary>
        /// driver declaration
        /// </summary>
        public static IWebDriver driver;

        /// <summary>
        /// Start Firefox browser
        /// </summary>
        [SetUp]
        public void Init()
        {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(SITEURL);
        }


        [Test]
        public void ProductContentVerification()
        {
            Footer footer = new Footer(driver);
            string firstProductTitle = footer.GetTitleText().TrimEnd('-', '.');
            ProductDescriptionPage prodPage = footer.GoToRandomProduct();
            StringAssert.StartsWith(firstProductTitle, prodPage.GetTitleText());
            Assert.IsNotEmpty(prodPage.GetDescriptionText());
            Assert.IsNotEmpty(prodPage.GetTextOfPeopleWhoBoughtSection());
        }

        [Test]
        public void SearchFunctionalityVerification()
        {

        }

        [Test]
        public void PictureEnlargementVerification()
        {

        }

        [Test]
        public void VerificationOfLikeFunctionality()
        {

        }
        /// <summary>
        /// Close current opened tab in browser
        /// </summary>
        [TearDown]
        public void Close()
        {
            driver.Quit();
        }

        
    }
}
