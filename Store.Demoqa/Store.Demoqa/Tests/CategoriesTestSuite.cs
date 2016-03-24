using NUnit.Framework;
using Store.Pages;

namespace Store.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class CategoriesTestSuite : BaseTest
    {
        private string productCategory = "iPhones";

        /// <summary>
        /// Selects Product Category and verifying of content view
        /// </summary>
        [Test]
        public void SelectCategoryVerification()
        {
            CategoryProductPage content = homePage.Header.SelectProductCategory(productCategory);
            CheckProductCategoryNameEqualsToCategoryTitle(productCategory, content);
            CheckListViewIsEnabled(content);
        }

        /// <summary>
        /// Checks the product category name equals to category title.
        /// </summary>
        /// <param name="productCategory">The product category.</param>
        /// <param name="content">The content.</param>
        private void CheckProductCategoryNameEqualsToCategoryTitle(string productCategory, CategoryProductPage content)
        {
            Assert.AreEqual(productCategory, content.CategoryTitle.Text);
        }

        /// <summary>
        /// Checks the ListView is enabled.
        /// </summary>
        /// <param name="content">The content.</param>
        private void CheckListViewIsEnabled(CategoryProductPage content)
        {
            Assert.That(content.DefaultListView.Enabled);
        }
    }
}
