using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Store.Demoqa.Pages;
using OpenQA.Selenium.Support.UI;
using System.Drawing;

namespace Store.Demoqa
{
    /// <summary>
    /// Store.Demoqa Tests
    /// </summary>
    [TestFixture]
    public class TestSuite
    {
        public string oneProduct = "magic mouse";
        public string multipleProducts = "iphone";
        private string userNAme = "qa29";
        private string password = "W0fucGsTDnVS";
        private string expectedUserGreeting = "Howdy, Qa29";
        private string productCategory = "iPhones";
        private int productIndex = 0;

        private const string SITEURL = "http://store.demoqa.com/";
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
            this.driver.Navigate().GoToUrl(SITEURL);
            this.driver.Manage().Window.Maximize();
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
            Header header = new Header(driver);
            ContentContainer searchResultsForOneProd = header.SetSearchValueAndSubmit(oneProduct);
            StringAssert.AreEqualIgnoringCase(searchResultsForOneProd.FoundProducts[0].Text, oneProduct);
            Assert.AreEqual(searchResultsForOneProd.FoundProducts.Count, 1);
            ContentContainer searchResultsForMultipleProd = header.SetSearchValueAndSubmit(multipleProducts);
            Assert.Greater(searchResultsForMultipleProd.FoundProducts.Count, 1);
        }

        [Test]
        public void PictureEnlargementVerification()
        {
            Header header = new Header(driver);
            ContentContainer homeContainer = header.GoToHomePage();
            string firstHomeProdTitle = homeContainer.HomeProdTitle.Text;
            ProductDescriptionPage product = homeContainer.GoToProdFromHomePage();
            StringAssert.AreEqualIgnoringCase(product.GetTitleText(), firstHomeProdTitle);
            product.ProdClosedImage[0].Click();
            Assert.IsNotNull(product.ProdOpenedImage);
            Size regularSize = product.ProdOpenedImage.Size;
            product.NextImageArrow.Click();
            Assert.IsNotNull(product.ProdOpenedImage);
            //product.EnlargeImage();
            Size enlargedSize = product.ProdOpenedImage.Size;
            Assert.Greater(enlargedSize.Height, regularSize.Height);
            Assert.Greater(enlargedSize.Width, regularSize.Width);
        }

        [Test]
        public void VerificationOfLikeFunctionality()
        {
            Header header = new Header(driver);
            ContentContainer homeContainer = header.GoToHomePage();
            ProductDescriptionPage product = homeContainer.GoToProdFromHomePage();
            //product.FBLikeButton.Click();
            driver.SwitchTo().Window(driver.WindowHandles[1]).Close();
            Assert.LessOrEqual(driver.WindowHandles.Count, 1);
        }
        /// <summary>
        /// Login with valid data 
        /// </summary>
        [Test]
        public void LoginVerification()
        {
            Login login = new Login(this.driver);
            Header header = new Header(this.driver);
            header.MyAccountButton.Click();
            login.SetUserName(userNAme);
            login.SetPassword(password);
            login.LoginButton.Click();
            this.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            Assert.AreEqual(expectedUserGreeting, header.HomePageUserName.Text);
            header.LogoutButton.Click();
        }

        /// <summary>
        /// Selects Product Category and verifying of content view
        /// </summary>
        [Test]
        public void SelectCategoryVerification()
        {
            Header header = new Header(this.driver);
            ContentContainer content = header.SelectProductCategory(productCategory);
            Assert.AreEqual(productCategory, content.PageHeader.Text);
            Assert.That(content.DefaultListView.Enabled);
        }

        /// <summary>
        /// Selects product by index and adding it to the cart 
        /// </summary>
        
        [Test]
        public void AddProductToCartVerification()
        {
            Header header = new Header(this.driver);
            ContentContainer content = header.SelectProductCategory(productCategory);
            string prodTitle = content.GetProductTitle(productIndex);
            AddToCartPopUp popUp = content.AddProductToTheCart(productIndex);
            popUp.ContinueShoppingButton.Click();
            this.driver.Navigate().Refresh();
            Assert.AreEqual("1", header.ItemsButton.Text);
            Cart cart = header.GoToCart();
            Assert.AreEqual(prodTitle, cart.GetProductInCart(prodTitle));
        }

        /// <summary>
        /// Close current opened tab in browser
        /// </summary>
        [TearDown]
        public void Close()
        {
            this.driver.Quit();
        }
    }
}
