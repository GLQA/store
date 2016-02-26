using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Store.Demoqa
{
    public class ProductDescriptionPage
    {
        private IWebDriver driver;

        public ProductDescriptionPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".prodtitle")]
        private IWebElement ProductTitle;

        [FindsBy(How = How.CssSelector, Using = ".product_description")]
        private IWebElement ProductDescription;

        [FindsBy(How = How.CssSelector, Using = ".wpsc_also_bought.group")]
        private IWebElement PeopleWhoBoughtThisItemSection;

        public string GetTitleText()
        {
            return ProductTitle.Text;
        }

        public string GetDescriptionText()
        {
            return ProductDescription.Text;
        }

        public string GetTextOfPeopleWhoBoughtSection()
        {
            return PeopleWhoBoughtThisItemSection.Text;
        }
    }
}