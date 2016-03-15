using NUnit.Framework;
using Store.Demoqa.PopUps;
using System;

namespace Store.Demoqa.Tests
{
    [TestFixture]
    public class CartTestSuite : BaseTest
    {
        private string productCategory = "iPhones";

        /// <summary>
        /// Selects product by index and adding it to the cart 
        /// </summary>  
        [Test]
        public void AddRandomProductToCartVerification()
        {
            CategoryProductPage content = homePage.Header.SelectProductCategory(productCategory);
            int productIndex = content.RandNumberOfProductInCategory();
            string prodTitle = content.GetProductTitleByIndex(productIndex);
            AddToCartPopUp popUp = content.AddProductToTheCart(productIndex);
            popUp.ContinueShoppingButton.Click();
            DriverSingleton.Instance.Driver.Navigate().Refresh();
            CheckNumberOfAddedProductsEqualsToNumberOfItemsInCart(homePage);
            CartPage cart = homePage.Header.GoToCart();
            CheckProductTitleEqualsToProductTitleInCart(prodTitle, cart);
        }

        /// <summary>
        /// Checks the number of added products equals to number of items in cart.
        /// </summary>
        /// <param name="homePage">The home page.</param>
        private void CheckNumberOfAddedProductsEqualsToNumberOfItemsInCart(HomePage homePage)
        {
            Assert.AreEqual("1", homePage.Header.ItemsButton.Text);
        }

        /// <summary>
        /// Checks the product title equals to product title in cart.
        /// </summary>
        /// <param name="prodTitle">The product title.</param>
        /// <param name="cart">The cart.</param>
        private void CheckProductTitleEqualsToProductTitleInCart(string prodTitle, CartPage cart)
        {
            Assert.IsTrue(cart.GetProductFromCart(prodTitle));
        }
    }
}
