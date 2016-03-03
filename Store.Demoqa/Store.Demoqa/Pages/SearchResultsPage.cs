using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Store.Demoqa
{
    public class SearchResultsPage : PageFrame
    {
        /// <summary>
        /// Gets the first found product title.
        /// </summary>
        /// <value>
        /// The first found product title.
        /// </value>
        public string FirstFoundProductTitle
        {
            get
            {
                return FoundProducts[0].Text;
            }
        }
        /// <summary>
        /// The found products
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".prodtitle")]
        public IList<IWebElement> FoundProducts { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultsPage"/> class.
        /// </summary>
        public SearchResultsPage() : base()
        {
            PageFactory.InitElements(Driver.Instance.driver, this);
        }
    }
}
