using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa
{
    public class HomePage : PageFrame
    {
        /// <summary>
        /// Gets or sets the home product reference.
        /// </summary>
        /// <value>
        /// The home product reference.
        /// </value>
        [FindsBy(How = How.CssSelector, Using = ".featured_image>a[href]")]
        public IWebElement HomeProductReference { get; set; }

        /// <summary>
        /// Gets or sets the home product title.
        /// </summary>
        /// <value>
        /// The home product title.
        /// </value>
        [FindsBy(How = How.CssSelector, Using = ".product_description>h2")]
        public IWebElement HomeProductTitle { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        public HomePage() : base()
        {
            PageFactory.InitElements(Driver.Instance.driver, this);
        }

        /// <summary>
        /// Goes to product from carousel.
        /// </summary>
        /// <returns></returns>
        public ProductDescriptionPage GoToProductFromCarousel()
        {
            HomeProductReference.Click();
            return new ProductDescriptionPage();
        }

    }
}
