//ShopPage Get rid of mouse move, generic method for adding to cart

using OpenQA.Selenium;
using static uk.co.nfocus.FullProject.Utilities.HelperLib;

namespace FullProject.POMs
{
    class ShopPage
    {
        private IWebDriver driver;

        public ShopPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators for elements on the 'Shop' Page
        public IWebElement InitialCartContents => driver.FindElement(By.CssSelector("a[title='View your shopping cart'] span[class='count']"));


        //Procedures for the 'Shop' Page
        public void AddItemToCart(string itemName)//Adds an item to the cart via linkText
        {
            Console.WriteLine($"a[aria-label='Add “{itemName}” to your cart']");
            driver.FindElement(By.CssSelector($"a[aria-label='Add “{itemName}” to your cart']")).Click();
        }
        public string GetCartContents()//Fetches the number of items currently in the cart
        {
            return driver.FindElement(By.CssSelector("a[title='View your shopping cart'] span[class='count']")).Text;
        }
        public void WaitForCartUpdate(string CC)//Waits for the cart to update
        {
            WaitForElementNotEquals(driver, By.CssSelector("a[title='View your shopping cart'] span[class='count']"), CC);
        }


    }
}
