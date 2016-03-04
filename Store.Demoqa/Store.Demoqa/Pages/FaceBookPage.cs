using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa
{
    public class FaceBookLoginPage
    {
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
        public FaceBookLoginPage()
        {
            PageFactory.InitElements(DriverSingleton.Instance.Driver, this);
        }

        public void Close()
        {
            DriverSingleton.Instance.Driver.Close();
        }
    }
}