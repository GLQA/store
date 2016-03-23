using NUnit.Framework;
using Store.Demoqa.Pages;
using System.Collections.Generic;

namespace Store.Demoqa.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class SearchTestSuite : BaseTest
    {
        /// <summary>
        /// Class describing dataset used for 'Search Functionality Verification' data-driven test
        /// </summary>
        public class SearchTestDataSet
        {
            public string ValueToSearch { get; set; }
            public int ExpectedNumberOfFoundProducts { get; set; }
        }

        /// <summary>
        /// Set of data for 'Search Functionality Verification' data-driven test
        /// </summary>
        /// <returns>IEnumerable<object[]></returns>
        public static IEnumerable<object[]> ProductsForSearch()
        {
            return new[]
            {
                new object[]
                {
                    "Verification of ability to find one product as search result",
                    new SearchTestDataSet()
                    {
                        ValueToSearch = "magic mouse",
                        ExpectedNumberOfFoundProducts = 1
                    }
                },
                new object[]
                {
                    "Verification of ability to find more than one product as search result",
                    new SearchTestDataSet()
                    {
                        ValueToSearch = "iphone 4",
                        ExpectedNumberOfFoundProducts = 2
                    }
                }
            };
        }
        /// <summary>
        /// Verification of Search results: search of 'magic mouse' must return 1 product and search of 'iphone 4' nust return two items 
        /// </summary>
        [Test, TestCaseSource("ProductsForSearch")]
        public void FoundProductsNumberVerification(string iterationName, SearchTestDataSet dataSet)
        {
            SearchResultsPage searchResults = homePage.Header.TypeSearchValueAndSubmit(dataSet.ValueToSearch);
            CheckThatOnlyRequiredProductsWereFound(searchResults.GetFoundProductsTitles(), dataSet.ValueToSearch);
            CheckThatExpectedProductsNumberWasFound(dataSet.ExpectedNumberOfFoundProducts, searchResults.FoundProducts.Count);
        }

        /// <summary>
        /// Checks the that only required products were found.
        /// </summary>
        /// <param name="listOfFoundProducts"></param>
        /// <param name="expectedTitle"></param>
        private void CheckThatOnlyRequiredProductsWereFound(List<string> listOfFoundProducts, string expectedTitle)
        {
            foreach (string actualTitle in listOfFoundProducts)
                Assert.That(actualTitle, Contains.Substring(expectedTitle).IgnoreCase);
        }

        /// <summary>
        /// Checks the that expected products number was found.
        /// </summary>
        /// <param name="foundProductsNumber">The found products number.</param>
        private void CheckThatExpectedProductsNumberWasFound(int expectedQuantity, int actualQuantity)
        {
            Assert.AreEqual(expectedQuantity, actualQuantity);
        }
    }
}
