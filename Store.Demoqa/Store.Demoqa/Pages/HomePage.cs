using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.Demoqa.Helpers;
using Store.Demoqa.PageBaseComponents;
using Store.Demoqa.Tests;

namespace Store.Demoqa.Pages
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

        public HomePage() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        public HomePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Goes to product from carousel.
        /// </summary>
        /// <returns></returns>
        public ProductDescriptionPage GoToProductFromCarousel()
        {
            HomeProductReference.Click();
            return BaseTest.repository.Get<ProductDescriptionPage>();
        }

    }
}
