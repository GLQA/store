using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Store.Demoqa.Pages
{
    public class SearchResultsPage : PageFrame
    {
        private IWebDriver driver;

        public string FirstFoundProductTitle
        {
            get
            {
                return FoundProducts[0].Text;
            }
        }
        [FindsBy(How = How.CssSelector, Using = ".prodtitle")]
        public IList<IWebElement> FoundProducts;

        public List<string> GetFoundProductsTitles()
        {
            List<string> listOfFoundProductsTitles = new List<string>();
            foreach (IWebElement product in FoundProducts)
                listOfFoundProductsTitles.Add(product.Text);
            return listOfFoundProductsTitles;
        }

        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
    }
}
