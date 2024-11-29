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
        public IWebElement Home => driver.FindElement(By.LinkText("Home"));
        public IWebElement Shop => driver.FindElement(By.LinkText("Shop"));
        public IWebElement Cart => driver.FindElement(By.LinkText("Cart"));
        public IWebElement Checkout => driver.FindElement(By.LinkText("Checkout"));
        public IWebElement MyAccount => driver.FindElement(By.LinkText("My account"));
        public IWebElement Blog => driver.FindElement(By.LinkText("Blog"));
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
