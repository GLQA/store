using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa.Pages
{
    /// <summary>
    /// Class describes work with Add to Cart pop-up
    /// </summary>
    public class AddToCartPopUp
    {
        /// <summary>
        /// The add to cart popup
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='fancy_notification_content']")]
        public IWebElement AddToCartPopup;

        /// <summary>
        /// The pop up text field
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='fancy_notification_content']/span")]
        public IWebElement PopUpTextField;

        /// <summary>
        /// The check out button
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='fancy_notification_content']/a[1]")]
        public IWebElement CheckOutButton;

        /// <summary>
        /// The continue shopping button
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='fancy_notification_content']/a[2]")]
        public IWebElement ContinueShoppingButton;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddToCartPopUp"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public AddToCartPopUp(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
    }
}
