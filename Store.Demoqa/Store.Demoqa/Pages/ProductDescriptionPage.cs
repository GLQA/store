using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Store.Demoqa
{
    public class ProductDescriptionPage : PageFrame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDescriptionPage"/> class.
        /// </summary>
        public ProductDescriptionPage() : base()
        {
           PageFactory.InitElements(DriverSingleton.Instance.Driver, this);
        }

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
        [FindsBy(How = How.CssSelector, Using = ".pluginButtonLabel")]
        public IWebElement FBLikeButton { get; set; }

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
        /// Enlarges the image.
        /// </summary>
        public void EnlargeImage()
        {
            ExpandImageButton.Click();
        }

        public void OpenImage()
        {
            ClosedImage.Click();
        }
        /// <summary>
        /// Closes the secondary window.
        /// </summary>
        public void CloseSecondaryWindow()
        {
            DriverSingleton.Instance.Driver.SwitchTo().Window(DriverSingleton.Instance.Driver.WindowHandles[1]).Close();
        }
    }
}