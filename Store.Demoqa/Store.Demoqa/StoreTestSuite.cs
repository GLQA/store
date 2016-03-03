using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
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
        private string userNAme = "qa29";

        private string password = "W0fucGsTDnVS";

        private string expectedUserGreeting = "Howdy, Qa29";

        private string productCategory = "iPhones";

        private string productTitleToCheckImageEnlargement = "Skullcandy";

        private int productIndex = 0;

        private HomePage homePage;

        /// <summary>
        /// Class describing dataset used for 'Search Functionality Verification' data-driven test
        /// </summary>
        //TODO: rename this piece of shit
        public class SearchDataSet
        {
            public string ValueToSearch { get; set; }
            public int ExpectedNumberOfFoundProducts { get; set; }
        }

        /// <summary>
        /// Set of data for 'Search Functionality Verification' data-driven test
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> ProductsForSearch()
        {
            return new[]
            {
                new object[]
                {
                    "Verification of ability to find one product as search result",
                    new SearchDataSet()
                    {
                        ValueToSearch = "magic mouse",
                        ExpectedNumberOfFoundProducts = 1
                    }
                },
                new object[]
                {
                    "Verification of ability to find more than one product as search result",
                    new SearchDataSet()
                    {
                        ValueToSearch = "iphone 4",
                        ExpectedNumberOfFoundProducts = 2
                    }
                }
            };
        }

        /// <summary>
        /// Initialization of new homepage in each test
        /// </summary>
        [SetUp]
        public void Init()
        {
             homePage = new HomePage();
        }

        /// <summary>
        /// Verification of availability of 'People who bought this item' section and product description 
        /// </summary>
        [Test]
        public void ProductContentVerification()
        {
            string TrimmedRandProductTitle = homePage.footer.TrimmedRandProductTitle;
            ProductDescriptionPage prodPage = homePage.footer.GoToRandomProduct();
            CheckOpenedProdTitleContainsRequired(TrimmedRandProductTitle, prodPage.ProductTitleText);
            CheckDescriptionSectionIsNotEmpty(prodPage.ProductDescriptionText);
            CheckPeopleBoughtSectionIsNotEmpty(prodPage.PeopleBoughtSectionText);
        }
        //TODO: 2. make methods from assertions and put them after each test - Maryna and Yuliia
        private static void CheckOpenedProdTitleContainsRequired(string randomProductTitle, string pageProductTitle)
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
        [Test, TestCaseSource("ProductsForSearch")]
        public void FoundProductsNumberVerification(string iterationName, SearchDataSet dataSet )
        {
            SearchResultsPage searchResults = homePage.header.TypeSearchValueAndSubmit(dataSet.ValueToSearch);
            CheckThatExpectedProductsNumberWasFound(dataSet.ExpectedNumberOfFoundProducts, searchResults.FoundProducts.Count);
            CheckThatOnlyRequiredProductsWereFound(searchResults.GetFoundProductsTitles(), dataSet.ValueToSearch);
        }

        private static void CheckThatExpectedProductsNumberWasFound(int expectedQuantity, int actualQuantity)
        {
            Assert.AreEqual(expectedQuantity, actualQuantity);
        }

        private static void CheckThatOnlyRequiredProductsWereFound(List<string> listOfFoundProducts, string expectedTitle)
        {
            foreach (string actualTitle in listOfFoundProducts)
                Assert.That(actualTitle, Contains.Substring(expectedTitle).IgnoreCase);
        }

        /// <summary>
        /// Verification of the picture availability and functionality on the product's description page
        /// </summary>
        [Test]
        public void PictureEnlargementVerification()
        {
            //TODO: check image md5 - Maryna
            ProductDescriptionPage product = homePage.header.FindProductAndGoToTheFirst(productTitleToCheckImageEnlargement);
            CheckRequiredProductWasOpened(product.ProductTitleText, productTitleToCheckImageEnlargement);
            int closedImageHashCode = product.ClosedImage.GetHashCode();
            product.OpenImage();
            CheckImageIsDisplayed(product.OpenedImage);
            int openedImageHashCode = product.OpenedImage.GetHashCode();
            Assert.AreEqual(closedImageHashCode, openedImageHashCode);
            Size regularSize = product.OpenedImage.Size;
            product.EnlargeImage();
            Size enlargedSize = product.OpenedImage.Size;
            Assert.Greater(enlargedSize.Height, regularSize.Height);
            Assert.Greater(enlargedSize.Width, regularSize.Width);
            product.NextImageArrow.Click();
            Assert.IsTrue(product.OpenedImage.Displayed);
        }

        private static void CheckImageIsDisplayed(IWebElement productImage)
        {
            Assert.IsTrue(productImage.Displayed);
        }

        private void CheckRequiredProductWasOpened(string actualTitle, string expectedTitle)
        {
            Assert.That(actualTitle, Contains.Substring(expectedTitle).IgnoreCase);
        }


        /// <summary>
        /// Verification of the Facebook 'Like' button
        /// </summary>
        [Test]
        public void LikeFunctionalityVerification()
        {
            ProductDescriptionPage product = homePage.GoToProductFromCarousel();
            //TODO: find LIKE btn - Maryna
            product.FBLikeButton.Click();
            product.CloseSecondaryWindow();
            Assert.LessOrEqual(Driver.Instance.driver.WindowHandles.Count, 1);
        }

        /// <summary>
        /// Login with valid data 
        /// </summary>
        [Test]
        public void LoginVerification()
        {
            LoginPage login = new LoginPage();
            homePage.header.MyAccountButton.Click();
            login.ClearAndTypeUserName(userNAme);
            login.ClearAndTypePassword(password);
            login.LoginButton.Click();
            Driver.Instance.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            CheckThatUserIsLoggedIn(expectedUserGreeting, homePage);
            homePage.header.LogoutButton.Click();
        }
        /// <summary>
        /// Checks the that user is logged in.
        /// </summary>
        /// <param name="expectedUserGreeting">The expected user greeting.</param>
        /// <param name="homePage">The home page.</param>
        private static void CheckThatUserIsLoggedIn( string expectedUserGreeting, HomePage homePage)
        {
            Assert.AreEqual(expectedUserGreeting, homePage.header.HomePageUserName.Text);
        }

        /// <summary>
        /// Selects Product Category and verifying of content view
        /// </summary>
        [Test]
        public void SelectCategoryVerification()
        {
            CategoryProductPage content = homePage.header.SelectProductCategory(productCategory);
            CheckProductCategoryNameEqualsToCategoryTitle(productCategory, content);
            CheckListViewIsEnabled(content);
        }

        /// <summary>
        /// Checks the product category name equals to category title.
        /// </summary>
        /// <param name="productCategory">The product category.</param>
        /// <param name="content">The content.</param>
        private static void CheckProductCategoryNameEqualsToCategoryTitle(string productCategory, CategoryProductPage content)
        {
            Assert.AreEqual(productCategory, content.CategoryTitle.Text);
        }

        /// <summary>
        /// Checks the ListView is enabled.
        /// </summary>
        /// <param name="content">The content.</param>
        private static void CheckListViewIsEnabled(CategoryProductPage content)
        {
            Assert.That(content.DefaultListView.Enabled);
        }

        /// <summary>
        /// Selects product by index and adding it to the cart 
        /// </summary>  
        [Test]
        public void AddProductToCartVerification()
        {
            CategoryProductPage content = homePage.header.SelectProductCategory(productCategory);
            string prodTitle = content.GetProductTitle(productIndex);
            AddToCartPopUp popUp = content.AddProductToTheCart(productIndex);
            Driver.Instance.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            popUp.ContinueShoppingButton.Click();
            Driver.Instance.driver.Navigate().Refresh();
            CheckNumberOfAddedProductsEqualsToNumberOfItemsInCart(homePage);
            CartPage cart = homePage.header.GoToCart();
            CheckProductTitleEqualsToProductTitleInCart(prodTitle, cart);      
        }

        /// <summary>
        /// Checks the number of added products equals to number of items in cart.
        /// </summary>
        /// <param name="homePage">The home page.</param>
        private static void CheckNumberOfAddedProductsEqualsToNumberOfItemsInCart(HomePage homePage)
        {
            Assert.AreEqual("1", homePage.header.ItemsButton.Text);
        }

        /// <summary>
        /// Checks the product title equals to product title in cart.
        /// </summary>
        /// <param name="prodTitle">The product title.</param>
        /// <param name="cart">The cart.</param>
        private static void CheckProductTitleEqualsToProductTitleInCart(string prodTitle, CartPage cart)
        {
            Assert.AreEqual(prodTitle, cart.GetProductFromCart(prodTitle));
        }

        /// <summary>
        /// Close the browser
        /// </summary>
        [TearDown]
        public void CloseBrowser()
        {
            Driver.Instance.Close();
        }
    }
}
