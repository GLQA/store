using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Store.Demoqa
{
    public class SearchResultsPage : PageFrame
    {
        /// <summary>
        /// The found products
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".prodtitle")]
        public IList<IWebElement> FoundProducts { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".prodtitle>a[href]:first-of-type")]
        public IWebElement FirstFoundProductReference { get; set; }

        /// <summary>
        /// Gets the found products titles.
        /// </summary>
        /// <returns></returns>
        public List<string> GetFoundProductsTitles()
        {
            List<string> listOfFoundProductsTitles = new List<string>();
            foreach (IWebElement product in FoundProducts)
                listOfFoundProductsTitles.Add(product.Text);
            return listOfFoundProductsTitles;
        }

        public ProductDescriptionPage GoToTheFirstFoundProduct()
        {
            FirstFoundProductReference.Click();
            return new ProductDescriptionPage();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultsPage"/> class.
        /// </summary>
        public SearchResultsPage() : base()
        {
            PageFactory.InitElements(DriverSingleton.Instance.Driver, this);
        }
    }
}
