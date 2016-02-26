using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Store.Demoqa.Pages;
using OpenQA.Selenium.Support.UI;

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
        /// Open site http://store.demoqa.com/
        /// </summary>
        [SetUp]
        public void Init()
        {
            this.driver = new FirefoxDriver();
            this.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            this.driver.Navigate().GoToUrl("http://store.demoqa.com/");
            this.driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Valid login 
        /// </summary>
        [Test]
        public void ValidLoginTest()
        {
            LoginPage login = new LoginPage(this.driver);
            Header header = new Header(this.driver);
            header.MyAccountButton.Click();
            login.SetUserName("qa29");
            login.SetPassword("W0fucGsTDnVS");
            login.LoginButton.Click();
            this.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            Assert.AreEqual("Howdy, Qa29", header.HomePageUserName.Text);
        }

        [Test]
        public void SelectCategoryTest()
        {
            string productCategory = "iMacs";
            Header header = new Header(this.driver);
            ContentContainer content = header.SelectProductCategory(productCategory);
            Assert.AreEqual(productCategory, content.PageHeader.Text);
            Assert.That(content.defaultListView.Enabled);
        }

        [Test]
        public void AddProductToCart()
        {
            string productCategory = "iPads";
            int productIndex = 1;
            Header header = new Header(this.driver);
            header.SelectProductCategory(productCategory);
            ContentContainer content = new ContentContainer(this.driver);
            String prodTitle = content.GetProductTitle(productIndex);
            AddToCartPopUp popUp = content.AddProductToTheCart(productIndex);
            popUp.continueShoppingButton.Click();
            driver.Navigate().Refresh();
            Cart cart = header.GoToCart();
            Assert.AreEqual(prodTitle, cart.firstProductInCart.Text);
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
