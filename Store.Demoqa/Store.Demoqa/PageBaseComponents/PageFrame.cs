using OpenQA.Selenium;
using Store.Demoqa.Helpers;

namespace Store.Demoqa.PageBaseComponents
{
    public class PageFrame
    {
        private IWebDriver driver;
        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        public Footer Footer
        {
            get
            {
                return new Footer(driver);
            }
        }

        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        public Header Header
        {
            get
            {
                return new Header(driver);
            }
        }

        /// <summary>
        /// Refreshes current page
        /// </summary>
        public void RefreshPage()
        {
            driver.Navigate().Refresh();
        }

        public string GetPageURL()
        {
            return driver.Url;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PageFrame"/> class.
        /// </summary>
        public PageFrame(IWebDriver driver)
        {
            this.driver = driver;
        }

        public PageFrame(){ }
    }
}
      