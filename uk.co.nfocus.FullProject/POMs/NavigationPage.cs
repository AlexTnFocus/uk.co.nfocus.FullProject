using OpenQA.Selenium;

namespace FullProject.POMs
{
    class NavigationPage
    {
        private IWebDriver driver;

        public NavigationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators for the 'Navigation' Page
        public IWebElement Home => driver.FindElement(By.CssSelector("li[id='menu-item-42'] a"));
        public IWebElement Shop => driver.FindElement(By.CssSelector("li[id='menu-item-43'] a"));
        public IWebElement Cart => driver.FindElement(By.CssSelector("li[id='menu-item-44'] a"));
        public IWebElement Checkout => driver.FindElement(By.CssSelector("li[id='menu-item-45'] a"));
        public IWebElement MyAccount => driver.FindElement(By.CssSelector("li[id='menu-item-46'] a"));
        public IWebElement Blog => driver.FindElement(By.CssSelector("li[id='menu-item-47'] a"));
        public IWebElement DismissButton => driver.FindElement(By.LinkText("Dismiss"));

        //Functions for the 'Naviagtion' page
        public void GoHome()//Navigates to the home page
        {
            Home.Click();
        }
        public void GoShop()//Navigates to the shop page
        {
            Shop.Click();
        }
        public void GoCart()//Navigates to the cart page
        {
            Cart.Click();
        }
        public void GoCheckout()//Navigates to the checkout page
        {
            Checkout.Click();
        }
        public void GoMyAccount()//Navigates to the my account page
        {
            MyAccount.Click();
        }
        public void GoBlog()//Navigates to the blog page
        {
            Blog.Click();
        }
        public void ClickDismiss()
        {
            DismissButton.Click();
        }

    }
}
