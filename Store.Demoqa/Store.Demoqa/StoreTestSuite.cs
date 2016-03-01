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
    public class StoreTestSuite
    {
        public string oneProduct = "magic mouse";
        public string multipleProducts = "iphone";
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
            //can't check this for multiple products, because search results contain product that does not correspond to request
            Assert.AreEqual(searchResultsForOneProd.FoundProducts.Count, 1);
            ContentContainer searchResultsForMultipleProd = header.SetSearchValueAndSubmit(multipleProducts);
            Assert.Greater(searchResultsForMultipleProd.FoundProducts.Count, 1);
        }

        [Test]
        public void PictureEnlargementVerification()
        {
            //how should I varify "image itself"?
            //there is no product name on each image(prod must be hardcoded)
            Header header = new Header(driver);
            ContentContainer homeContainer = header.GoToHomePage();
            string firstHomeProdTitle = homeContainer.HomeProdTitle.Text;
            ProductDescriptionPage product = homeContainer.GoToProdFromHomePage();
            StringAssert.AreEqualIgnoringCase(product.GetTitleText(), firstHomeProdTitle);
            product.ProdRegularImage.Click();
            Assert.IsTrue(product.ProdOpenedImage.Displayed);
            Size regularSize = product.ProdOpenedImage.Size;
            product.NextImageArrow.Click();
            Assert.IsTrue(product.ProdOpenedImage.Displayed);
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
            this.driver.Quit();
        }
    }
}
