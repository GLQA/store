using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.Demoqa.Helpers;
using Store.Demoqa.PageBaseComponents;
using Store.Demoqa.Tests;
using System.Collections.Generic;
using System.Net;

namespace Store.Demoqa.Pages
{
    public class ProductDescriptionPage : PageFrame
    {
        private IWebDriver driver;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDescriptionPage"/> class.
        /// </summary>
        public ProductDescriptionPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public ProductDescriptionPage() : base() { }

        /// <summary>
        /// The product title
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".prodtitle")]
        public IWebElement ProductTitle { get; set; }

        /// <summary>
        /// The product description
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".product_description")]
        public IWebElement ProductDescription { get; set; }

        /// <summary>
        /// The product page images
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".product_image")]
        public IList <IWebElement> ProdPageImages { get; set; }

        /// <summary>
        /// The product description page main image
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".imagecol>a[href]")]
        public IWebElement MainImage { get; set; }

        /// <summary>
        /// The people who bought section
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".wpsc_also_bought.group")]
        public IWebElement PeopleWhoBoughtSection { get; set; }

        /// <summary>
        /// The product opened image
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#fullResImage")]
        public IWebElement OpenedImage { get; set; }

        /// <summary>
        /// The next image arrow
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".pp_next")]
        public IWebElement NextImageArrow { get; set; }

        /// <summary>
        /// The expand image button
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".pp_expand")]
        public IWebElement ExpandImageButton { get; set; }

        /// <summary>
        /// The fb likes counter
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".pluginCountTextDisconnected")]
        public IWebElement FBLikesCounter { get; set; }

        /// <summary>
        /// Gets or sets the fb like button.
        /// </summary>
        /// <value>
        /// The fb like button.
        /// </value>
        [FindsBy(How = How.CssSelector, Using = ".pluginButtonImage>button")]
        public IWebElement FaceBookLikeButton { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".fb-like.fb_iframe_widget>span>iframe")]
        public IWebElement FaceBookContainer { get; set; }

        /// <summary>
        /// Gets the product regular image.
        /// </summary>
        /// <value>
        /// The product regular image.
        /// </value>
        public IWebElement ClosedImage
        {
            get { return ProdPageImages[0]; }
        }

        /// <summary>
        /// Gets the product title text.
        /// </summary>
        /// <value>
        /// The product title text.
        /// </value>
        public string ProductTitleText
        {
            get { return ProductTitle.Text; }
        }

        /// <summary>
        /// Gets the product description text.
        /// </summary>
        /// <value>
        /// The product description text.
        /// </value>
        public string ProductDescriptionText
        {
            get { return ProductDescription.Text; }
        }

        /// <summary>
        /// Gets the people bought section text.
        /// </summary>
        /// <value>
        /// The people bought section text.
        /// </value>
        public string PeopleBoughtSectionText
        {
            get { return PeopleWhoBoughtSection.Text; }
        }

        /// <summary>
        /// Switches driver from main content to the Facebook iframe
        /// </summary>
        public void GoToFaceBookFrame()
        {
            driver.SwitchTo().Frame(FaceBookContainer);
        }

        /// <summary>
        /// Switches driver to Facebook iframe and click button 'Like'. Page is refreshing because after first clicking 'Like' button disappears. Returns new instance of Facebook login page 
        /// </summary>
        public void ClickFaceBookLikeButton()
        {
            GoToFaceBookFrame();
            FaceBookLikeButton.Click();
            if (driver.WindowHandles.Count == 1)
            {
                RefreshPage();
                ClickFaceBookLikeButton();
            }     
        }
        /// <summary>
        /// Enlarges the image.
        /// </summary>
        public void EnlargeImage()
        {
            ExpandImageButton.Click();
        }

        /// <summary>
        /// Opens image of the product
        /// </summary>
        public void OpenImage()
        {
            ClosedImage.Click();
        }

        /// <summary>
        /// Saves products image to fileLocationPath
        /// </summary>
        public void SaveImage(string fileLocationPath)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(GetElementReference(MainImage), fileLocationPath);
            }
        }

        /// <summary>
        /// Gets hyperlink reference of WebElement
        /// </summary>
        /// <returns></returns>
        public string GetElementReference(IWebElement element)
        {
            return element.GetAttribute("href");
        }

        /// <summary>
        /// Switches driver to the secondary window with Facebook login page
        /// </summary>
        public FaceBookLoginPage SwitchToFaceBookWindow()
        {
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            return BaseTest.repository.Get<FaceBookLoginPage>();
        }

        public int GetWindowsQuantity()
        {
            return driver.WindowHandles.Count;
        }
    }
}