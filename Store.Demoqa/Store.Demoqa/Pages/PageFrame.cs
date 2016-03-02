using OpenQA.Selenium;

namespace Store.Demoqa.Pages
{
    public class PageFrame
    {
        private IWebDriver driver;
        public Footer footer
        {
            get
            {
                return new Footer(driver);
            }
        }

        public Header header
        {
            get
            {
                return new Header(driver);
            }
        }

        public PageFrame(IWebDriver driver)
        {

        }
    }
}
      