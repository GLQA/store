using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa.PopUps
{
    /// <summary>
    /// Class describes work with Add to Cart pop-up
    /// </summary>
    public class AddToCartPopUp
    {
        /// <summary>
        /// The add to cart popup
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#fancy_notification_content")]
        public IWebElement AddToCartPopup { get; set; }

        /// <summary>
        /// The pop up text field
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#fancy_notification_content>span")]
        public IWebElement PopUpTextField { get; set; }

        /// <summary>
        /// The check out button
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='fancy_notification_content']/a[1]")]
        public IWebElement CheckOutButton { get; set; }

        /// <summary>
        /// The continue shopping button
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='fancy_notification_content']/a[2]")]
        public IWebElement ContinueShoppingButton { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddToCartPopUp"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public AddToCartPopUp()
        {
            PageFactory.InitElements(DriverSingleton.Instance.Driver, this);
        }
    }
}
