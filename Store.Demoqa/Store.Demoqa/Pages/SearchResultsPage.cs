using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Store.Demoqa.Helpers;
using Store.Demoqa.PageBaseComponents;
using Store.Demoqa.Tests;
using System.Collections.Generic;

namespace Store.Demoqa.Pages
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
            return BaseTest.repository.Get<ProductDescriptionPage>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultsPage"/> class.
        /// </summary>
        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public SearchResultsPage() : base() { }
    }
}
