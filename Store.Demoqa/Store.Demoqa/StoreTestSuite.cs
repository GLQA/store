using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Store.Demoqa.Pages;
using System.Drawing;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa
{
    /// <summary>
    /// Store.Demoqa Tests
    /// </summary>
    [TestFixture]
    public class StoreTestSuite
    {
        private string prodNameThatGivesOneSearchResult = "magic mouse";
        private string prodNameThatGivesSeveralSearchResult = "iphone 4";
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

          Driver.Instance.Start();
           
        }

        [Test]
        public void ProductContentVerification()
        {
            //TODO: 1. remove 'driver' parameter from constructors - make it as singleton - Yuliia
            //TODO: 1. create page structure(reorganize 'Pages' folder): BasePage contains Header and Footer , all other pages are inherited from BasePage - Maryna
            Footer footer = new Footer();
            //TODO: I want to understand, after several month, what I am doing here...why? - Maryna
            string randomProductTitle = footer.GetRandomProdTitleText().TrimEnd('-', '.');
            ProductDescriptionPage prodPage = footer.GoToRandomProduct();




            // Check that product title equals to opened
            StringAssert.StartsWith(randomProductTitle, prodPage.GetTitleText());
            // Check that descriptiona section is not empty
            Assert.IsNotEmpty(prodPage.GetDescriptionText());
            // Check that PeopleWhoBought section is not empty
            Assert.IsNotEmpty(prodPage.GetTextOfPeopleWhoBoughtSection());

            

            CheckProductTitleEqualsToOpened(randomProductTitle, prodPage);
            CheckDescriptionSectionIsNotEmpty(prodPage);
            CheckPeopleWhoBoughtSectionIsNotEmpty(prodPage);
        }
        //TODO: 2. make methods from assertions and put them after each test - Maryna and Yuliia
        private static void CheckProductTitleEqualsToOpened(string randomProductTitle, ProductDescriptionPage prodPage)
        {
            StringAssert.StartsWith(randomProductTitle, prodPage.GetTitleText());
        }

        private static void CheckDescriptionSectionIsNotEmpty(ProductDescriptionPage prodPage)
        {
            Assert.IsNotEmpty(prodPage.GetDescriptionText());
        }

        private static void CheckPeopleWhoBoughtSectionIsNotEmpty(ProductDescriptionPage prodPage)
        {
            Assert.IsNotEmpty(prodPage.GetTextOfPeopleWhoBoughtSection());
        }

        //TODO: maintain data-driven - Maryna
        [Test]
        public void SearchResultsVerification()
        {
            Header header = new Header();
            ContentContainer contentContainer = header.TypeSearchValueAndSubmit(prodNameThatGivesOneSearchResult);
            //TODO: extract method from contentContainer.FoundProducts[0].Text - Maryna
            StringAssert.AreEqualIgnoringCase(contentContainer.FoundProducts[0].Text, prodNameThatGivesOneSearchResult);
            //can't check this for multiple products, because search results contain product that does not correspond to request
            Assert.AreEqual(contentContainer.FoundProducts.Count, 1);
            //TODO: for multiple: StringAssert.AreEqualIgnoringCase(contentContainer.FoundProducts[0].Text, prodNameThatGivesOneSearchResult); - Maryna
            contentContainer = header.TypeSearchValueAndSubmit(prodNameThatGivesSeveralSearchResult);
        }

        [Test]
        public void PictureEnlargementVerification()
        {
            //TODO: check image md5 - Maryna
            //TODO: there is no product name on each image(prod must be hardcoded) - Maryna
            Header header = new Header();
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
            Header header = new Header();
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
            Login login = new Login();
            Header header = new Header();
            header.MyAccountButton.Click();
            login.ClearAndTypeUserName(userNAme);
            login.ClearAndTypePassword(password);
            login.LoginButton.Click();
           Driver.Instance.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            Assert.AreEqual(expectedUserGreeting, header.HomePageUserName.Text);
            header.LogoutButton.Click();
            
        }

        /// <summary>
        /// Selects Product Category and verifying of content view
        /// </summary>
        [Test]
        public void SelectCategoryVerification()
        {
            Header header = new Header();
            ContentContainer content = header.SelectProductCategory(productCategory);
            StoreTestSuite.CheckProductCategoryNameEqualsProductCategoryPageTitle(productCategory, content);
            StoreTestSuite.CheckThatListViewEnabled(content);
        }

        public static void CheckProductCategoryNameEqualsProductCategoryPageTitle(string productCategory, ContentContainer content)
        {
            Assert.AreEqual(productCategory, content.PageHeader.Text);
        }
        public static void CheckThatListViewEnabled(ContentContainer content)
        {
            Assert.That(content.DefaultListView.Enabled);
        }

        /// <summary>
        /// Selects product by index and adding it to the cart 
        /// </summary>
        //todo: "count items" verify how many were added and displayed
        [Test]
        public void AddProductToCartVerification()
        {

            Header header = new Header();
            ContentContainer content = header.SelectProductCategory(productCategory);
            string prodTitle = content.GetProductTitle(productIndex);
            AddToCartPopUp popUp = content.AddProductToTheCart(productIndex);
            popUp.ContinueShoppingButton.Click();
            this.driver.Navigate().Refresh();
            Assert.AreEqual("1", header.ItemsButton.Text);
            Cart cart = header.GoToCart();
            StoreTestSuite.CheckProductTileIContainerEqualsTitleInCart(cart, prodTitle);
        }

        public static void CheckProductTileIContainerEqualsTitleInCart(Cart cart,string prodTitle)
        {
            Assert.AreEqual(prodTitle, cart.GetProductFromCart(prodTitle));
        }

        /// <summary>
        /// Close current opened tab in browser
        /// </summary>
        [TearDown]
        public void Close()
        {
            Driver.Instance.Close();
        }
    }
}
