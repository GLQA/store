using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa.Pages
{
    public class HomePage : PageFrame
    {
        private IWebDriver driver;

        [FindsBy(How = How.CssSelector, Using = ".featured_image>a[href]")]
        public IWebElement HomeProductReference;

        [FindsBy(How = How.CssSelector, Using = ".product_description>h2")]
        public IWebElement HomeProductTitle;

        public HomePage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public ProductDescriptionPage GoToProductFromCarousel()
        {
            HomeProductReference.Click();
            return new ProductDescriptionPage(driver);
        }

    }
}
