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
        /// Starts Firefox browser, opens site "http://store.demoqa.com/" and maximizes window
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
            Login login = new Login(this.driver);
            Header header = new Header(this.driver);
            header.MyAccountButton.Click();
            login.SetUserName("qa29");
            login.SetPassword("W0fucGsTDnVS");
            login.LoginButton.Click();
            this.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            Assert.AreEqual("Howdy, Qa29", header.HomePageUserName.Text);
        }

        /// <summary>
        /// Selects Product Category and verifying of content view
        /// </summary>
        [Test]
        public void SelectCategoryTest()
        {
            string productCategory = "iMacs";
            Header header = new Header(this.driver);
            ContentContainer content = header.SelectProductCategory(productCategory);
            Assert.AreEqual(productCategory, content.PageHeader.Text);
            Assert.That(content.DefaultListView.Enabled);
        }

        /// <summary>
        /// Selects product by index and adding it to the cart 
        /// </summary>
        [Test]
        public void AddProductToCart()
        {
            string productCategory = "iPhones";
            int productIndex = 0;
            Header header = new Header(this.driver);
            ContentContainer content = header.SelectProductCategory(productCategory);
            string prodTitle = content.GetProductTitle(productIndex);
            AddToCartPopUp popUp = content.AddProductToTheCart(productIndex);
            popUp.ContinueShoppingButton.Click();
            this.driver.Navigate().Refresh();
            string numberOfItemsAfterAdding = header.ItemsButton.Text;
            Assert.AreEqual("1", numberOfItemsAfterAdding);
            Cart cart = header.GoToCart();
            Assert.AreEqual(prodTitle, cart.FirstProductInCart.Text);
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
