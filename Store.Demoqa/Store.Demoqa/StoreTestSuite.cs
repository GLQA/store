using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Drawing;
using System.Collections.Generic;
using Store.Demoqa.Pages;

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
        private string productToCheckImageEnlargement = "Skullcandy";
        private string expectedLogInLogOutLinkName = "Log in";
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
            ProductDescriptionPage prodPage = homePage.Footer.GoToRandomProduct();
            CheckProductTitleEqualsToOpened(homePage.Footer.TrimmedRandProductTitle, prodPage.ProductTitleText);
            CheckDescriptionSectionIsNotEmpty(prodPage.ProductDescriptionText);
            CheckPeopleBoughtSectionIsNotEmpty(prodPage.PeopleBoughtSectionText);
        }
        //TODO: remove static from assertions - Maryna and Yuliia
        /// <summary>
        /// Checks the product title equals to opened.
        /// </summary>
        /// <param name="randomProductTitle">The random product title.</param>
        /// <param name="pageProductTitle">The page product title.</param>
        private static void CheckProductTitleEqualsToOpened(string randomProductTitle, string pageProductTitle)
        {
            StringAssert.StartsWith(randomProductTitle, pageProductTitle);
        }

        /// <summary>
        /// Checks the description section is not empty.
        /// </summary>
        /// <param name="productDescription">The product description.</param>
        private static void CheckDescriptionSectionIsNotEmpty(string productDescription)
        {
            Assert.IsNotEmpty(productDescription);
        }
        /// <summary>
        /// Checks the people bought section is not empty.
        /// </summary>
        /// <param name="sectionText">The section text.</param>
        private static void CheckPeopleBoughtSectionIsNotEmpty(string sectionText)
        {
            Assert.IsNotEmpty(sectionText);
        }

        /// <summary>
        /// Verification of Search results: search of 'magic mouse' must return 1 product and search of 'iphone 4' nust return two items 
        /// </summary>
        [Test, TestCaseSource("ProductsForSearch")]
        public void SearchResultsVerification(string iterationName, DataSetForSearchFunctionalityVerification dataSet )
        {
            SearchResultsPage searchResults = homePage.Header.TypeSearchValueAndSubmit(dataSet.ValueToSearch);
           // CheckThatOnlyRequiredProductsWereFound(searchResults.FirstFoundProductTitle, dataSet.ValueToSearch);
            CheckThatExpectedProductsNumberWasFound(dataSet.ExpectedNumberOfFoundProducts);
            List<string> listOfFoundProductsTitles = searchResults.GetFoundProductsTitles();
            foreach (string prodTitle in listOfFoundProductsTitles)
            StringAssert.Contains(dataSet.ValueToSearch, prodTitle);
        }

        /// <summary>
        /// Checks the that expected products number was found.
        /// </summary>
        /// <param name="foundProductsNumber">The found products number.</param>
        private static void CheckThatExpectedProductsNumberWasFound(int foundProductsNumber)
        {
            Assert.AreEqual(foundProductsNumber, 1);
        }

        /// <summary>
        /// Checks the that only required products were found.
        /// </summary>
        /// <param name="foundProductTitle">The found product title.</param>
        /// <param name="requiredProductTitle">The required product title.</param>
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
            ProductDescriptionPage product = homePage.Header.FindProductAndGoToTheFirst(productToCheckImageEnlargement);
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

        /// <summary>
        /// Verification of the Facebook 'Like' button
        /// </summary>
        [Test]
        public void LikeFunctionalityVerification()
        {
            ProductDescriptionPage product = homePage.GoToProductFromCarousel();
            product.FBLikeButton.Click();
            product.CloseSecondaryWindow();
            Assert.LessOrEqual(DriverSingleton.Instance.Driver.WindowHandles.Count, 1);
        }

        /// <summary>
        /// Login with valid data 
        /// </summary>
        [Test]
        public void LoginAndLogoutVerification()
        {
            LoginPage login = new LoginPage();
            homePage.Header.MyAccountButton.Click();
            login.ClearAndTypeUserName(userNAme);
            login.ClearAndTypePassword(password);
            login.LoginButton.Click();
            DriverSingleton.Instance.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            CheckThatUserIsLoggedIn(expectedUserGreeting, homePage);
            homePage.Header.LogoutButton.Click();
            DriverSingleton.Instance.Driver.Navigate().Refresh();
            Meta meta = new Meta();
            CheckThatUserLoggedOut(expectedLogInLogOutLinkName, meta);
        }

        /// <summary>
        /// Checks the that user logged out.
        /// </summary>
        /// <param name="meta">The meta.</param>
        private void CheckThatUserLoggedOut(string expectedLogInLogOutLinkName, Meta meta)
        {
            Assert.AreEqual(expectedLogInLogOutLinkName, meta.LogInLogOutLink.Text);
        }
        /// <summary>
        /// Checks the that user is logged in.
        /// </summary>
        /// <param name="expectedUserGreeting">The expected user greeting.</param>
        /// <param name="homePage">The home page.</param>
        private void CheckThatUserIsLoggedIn( string expectedUserGreeting, HomePage homePage)
        {
            Assert.AreEqual(expectedUserGreeting, homePage.Header.HomePageUserName.Text);
        }

        /// <summary>
        /// Selects Product Category and verifying of content view
        /// </summary>
        [Test]
        public void SelectCategoryVerification()
        {
            CategoryProductPage content = homePage.Header.SelectProductCategory(productCategory);
            CheckProductCategoryNameEqualsToCategoryTitle(productCategory, content);
            CheckListViewIsEnabled(content);
        }

        /// <summary>
        /// Checks the product category name equals to category title.
        /// </summary>
        /// <param name="productCategory">The product category.</param>
        /// <param name="content">The content.</param>
        private void CheckProductCategoryNameEqualsToCategoryTitle(string productCategory, CategoryProductPage content)
        {
            Assert.AreEqual(productCategory, content.CategoryTitle.Text);
        }

        /// <summary>
        /// Checks the ListView is enabled.
        /// </summary>
        /// <param name="content">The content.</param>
        private void CheckListViewIsEnabled(CategoryProductPage content)
        {
            Assert.That(content.DefaultListView.Enabled);
        }

        /// <summary>
        /// Selects product by index and adding it to the cart 
        /// </summary>  
        [Test]
        public void AddRandomProductToCartVerification()
        {
            CategoryProductPage content = homePage.Header.SelectProductCategory(productCategory);
            int productIndex = content.RandNumberOfProductInCategory();
            string prodTitle = content.GetProductTitleByIndex(productIndex);
            AddToCartPopUp popUp = content.AddProductToTheCart(productIndex);
            DriverSingleton.Instance.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            popUp.ContinueShoppingButton.Click();
            DriverSingleton.Instance.Driver.Navigate().Refresh();
            CheckNumberOfAddedProductsEqualsToNumberOfItemsInCart(homePage);
            CartPage cart = homePage.Header.GoToCart();
            CheckProductTitleEqualsToProductTitleInCart(prodTitle, cart);      
        }

        /// <summary>
        /// Checks the number of added products equals to number of items in cart.
        /// </summary>
        /// <param name="homePage">The home page.</param>
        private void CheckNumberOfAddedProductsEqualsToNumberOfItemsInCart(HomePage homePage)
        {
            Assert.AreEqual("1", homePage.Header.ItemsButton.Text);
        }

        /// <summary>
        /// Checks the product title equals to product title in cart.
        /// </summary>
        /// <param name="prodTitle">The product title.</param>
        /// <param name="cart">The cart.</param>
        private void CheckProductTitleEqualsToProductTitleInCart(string prodTitle, CartPage cart)
        {
            Assert.IsTrue(cart.GetProductFromCart(prodTitle));
        }

        //TODO: create base test, base tear down - Maryna
        /// <summary>
        /// Closes the browser
        /// </summary>
        [TearDown]
        public void CloseBrowser()
        {
            DriverSingleton.Instance.Close();
        }
        //TODO: HTMLElements - Yuliia
        //ToDO: install selenium grid; two processes(two singletons) similar parts(two solutions: thread local, delete singleton) - Maryna
        //TODO: create screen repository - Maryna
        //TODO: reorganize tests according to functionality - Yuliia
        //TODO: reorganize pages - Maryna 
    }
}
