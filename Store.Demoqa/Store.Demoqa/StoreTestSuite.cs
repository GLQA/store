using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Store.Demoqa.Pages;
using System.Drawing;
using System.Collections.Generic;

namespace Store.Demoqa
{
    /// <summary>
    /// Store.Demoqa Tests
    /// </summary>
    [TestFixture]
    public class StoreTestSuite
    {
        private string productTitleThatGivesOneSearchResult = "magic mouse";
        private string prodNameThatGivesSeveralSearchResult = "iphone 4";
        private string userNAme = "qa29";
        private string password = "W0fucGsTDnVS";
        private string expectedUserGreeting = "Howdy, Qa29";
        private string productCategory = "iPhones";
        private string productToCheckImageEnlargement = "Skullcandy";
        private int productIndex = 0;
        private HomePage homePage;
        public class DataSetForSearchFunctionalityVerification
        {
            public string ValueToSearch { get; set; }
            public int ExpectedNumberOfFoundProducts { get; set; }
        }
        public static IEnumerable<object[]> ProductsForSearch()
        {
            return new[]
            {
                new object[]
                {
                    "Verification of ability to find one product as search result",
                    new DataSetForSearchFunctionalityVerification()
                    {
                        ValueToSearch = "magic mouse",
                        ExpectedNumberOfFoundProducts = 1
                    }
                },
                new object[]
                {
                    "Verification of ability to find more than one product as search result",
                    new DataSetForSearchFunctionalityVerification()
                    {
                        ValueToSearch = "iphone 4",
                        ExpectedNumberOfFoundProducts = 2
                    }
                }
            };
        }

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
            HomePage homePage = new HomePage(driver);
        }

        /// <summary>
        /// Verification of availability of 'People who bought this item' section and product description 
        /// </summary>
        [Test]
        public void ProductContentVerification()
        {
            //TODO: 1. remove 'driver' parameter from constructors - make it as singleton - Yuliia
            ProductDescriptionPage prodPage = homePage.footer.GoToRandomProduct();
            CheckProductTitleEqualsToOpened(homePage.footer.TrimmedRandProductTitle, prodPage.ProductTitleText);
            CheckDescriptionSectionIsNotEmpty(prodPage.ProductDescriptionText);
            CheckPeopleBoughtSectionIsNotEmpty(prodPage.PeopleBoughtSectionText);
        }
        //TODO: 2. make methods from assertions and put them after each test - Maryna and Yuliia
        private static void CheckProductTitleEqualsToOpened(string randomProductTitle, string pageProductTitle)
        {
            StringAssert.StartsWith(randomProductTitle, pageProductTitle);
        }

        private static void CheckDescriptionSectionIsNotEmpty(string productDescription)
        {
            Assert.IsNotEmpty(productDescription);
        }

        private static void CheckPeopleBoughtSectionIsNotEmpty(string sectionText)
        {
            Assert.IsNotEmpty(sectionText);
        }

        /// <summary>
        /// Verification of Search results: search of 'magic mouse' must return 1 product and search of 'iphone 4' nust return two items 
        /// </summary>
        //TODO: maintain data-driven - Maryna
        [Test, TestCaseSource("ProductsForSearch")]
        public void SearchResultsVerification(string iterationName, DataSetForSearchFunctionalityVerification dataSet )
        {
            SearchResultsPage searchResults = homePage.header.TypeSearchValueAndSubmit(dataSet.ValueToSearch);
            CheckThatOnlyRequiredProductsWereFound(searchResults.FirstFoundProductTitle, dataSet.ValueToSearch);
            CheckThatExpectedProductsNumberWasFound(dataSet.ExpectedNumberOfFoundProducts);
            List<string> listOfFoundProductsTitles = searchResults.GetFoundProductsTitles();
            foreach (string prodTitle in listOfFoundProductsTitles)
                StringAssert.Contains(dataSet.ValueToSearch, prodTitle);
        }

        private static void CheckThatExpectedProductsNumberWasFound(int foundProductsNumber)
        {
            Assert.AreEqual(foundProductsNumber, 1);
        }

        private void CheckThatOnlyRequiredProductsWereFound(string foundProductTitle, string requiredProductTitle)
        {
            
            StringAssert.AreEqualIgnoringCase(foundProductTitle, requiredProductTitle);
        }

        /// <summary>
        /// Verification of the picture availability and functionality on the product's description page
        /// </summary>
        [Test]
        public void PictureEnlargementVerification()
        {
            //TODO: check image md5 - Maryna
            ProductDescriptionPage product = homePage.header.FindProductAndGoToTheFirst(productToCheckImageEnlargement);
            StringAssert.AreEqualIgnoringCase(product.ProductTitleText, productToCheckImageEnlargement); 
            product.ProductRegularImage.Click();
            Assert.IsTrue(product.ProductOpenedImage.Displayed);
            //check md5
            Size regularSize = product.ProductOpenedImage.Size;
            product.EnlargeImage();
            Size enlargedSize = product.ProductOpenedImage.Size;
            Assert.Greater(enlargedSize.Height, regularSize.Height);
            Assert.Greater(enlargedSize.Width, regularSize.Width);
            product.NextImageArrow.Click();
            Assert.IsTrue(product.ProductOpenedImage.Displayed);
            //check md5
        }

        //TODO: add summaries everywhere! - Maryna

        /// <summary>
        /// Verification of the Facebook 'Like' button
        /// </summary>
        [Test]
        public void LikeFunctionalityVerification()
        {
            ProductDescriptionPage product = homePage.GoToProductFromCarousel();
            product.FBLikeButton.Click();
            product.CloseSecondaryWindow();
            Assert.LessOrEqual(driver.WindowHandles.Count, 1);
        }

        /// <summary>
        /// Login with valid data 
        /// </summary>
        [Test]
        public void LoginVerification()
        {
            LoginPage login = new LoginPage(this.driver);
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
            CategoryProductPage content = header.SelectProductCategory(productCategory);
            Assert.AreEqual(productCategory, content.CategoryTitle.Text);
            Assert.That(content.DefaultListView.Enabled);
        }

        /// <summary>
        /// Selects product by index and adding it to the cart 
        /// </summary>
        
        [Test]
        public void AddProductToCartVerification()
        {
            Header header = new Header(this.driver);
            CategoryProductPage content = header.SelectProductCategory(productCategory);
            string prodTitle = content.GetProductTitle(productIndex);
            AddToCartPopUp popUp = content.AddProductToTheCart(productIndex);
            popUp.ContinueShoppingButton.Click();
            this.driver.Navigate().Refresh();
            Assert.AreEqual("1", header.ItemsButton.Text);
            CartPage cart = header.GoToCart();
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
