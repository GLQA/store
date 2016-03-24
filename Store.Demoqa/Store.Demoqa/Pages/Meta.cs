using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.PageBaseComponents;

namespace Store.Pages
{
    public class Meta : PageFrame
    {
        /// <summary>
        /// Gets or sets the logout button.
        /// </summary>
        /// <value>
        /// The logout button.
        /// </value>
        [FindsBy(How = How.CssSelector, Using = "#meta>ul>li:nth-of-type(2)>a")]
        public IWebElement LogInLogOutLink { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meta"/> class.
        /// </summary>
        public Meta(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public Meta() : base() { }
    }
}
