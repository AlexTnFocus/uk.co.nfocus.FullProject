using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IWebElement Blog => driver.FindElement(By.CssSelector("li[id='menu-item-46'] a"));

        //Functions for the 'Naviagtion' page
        public void GoHome()
        {
            Home.Click();
        }
        public void GoShop()
        {
            Shop.Click();
        }
        public void GoCart()
        {
            Cart.Click();
        }
        public void GoCheckout()
        {
            Checkout.Click();
        }
        public void GoMyAccount()
        {
            MyAccount.Click();
        }
        public void GoBlog()
        {
            Blog.Click();
        }

    }
}
