using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.Demoqa.Helpers;
using Store.Demoqa.PageBaseComponents;

namespace Store.Demoqa.Pages
{
    public class FaceBookLoginPage : PageFrame
    {
        private IWebDriver driver;

        [FindsBy(How = How.CssSelector, Using = "#homelink")]
        public IWebElement PageTitle { get; set; }

        /// <summary>
        /// Returns Facebook login page title
        /// </summary>
        public string TitleText
        {
            get
            {
                return PageTitle.Text;
            }
        }
        /// <summary>
        /// Creates an instance of FaceBookLoginPage
        /// </summary>
        public FaceBookLoginPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public FaceBookLoginPage(): base(){ }

        public void Close()
        {
            driver.Close();
        }
    }
}