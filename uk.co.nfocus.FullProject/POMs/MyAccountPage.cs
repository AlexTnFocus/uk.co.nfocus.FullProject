//MyAccountPage shorten css selectors if possible, privatise locators

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullProject.POMs
{
    class MyAccountPage
    {
        private IWebDriver driver;

        public MyAccountPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators for elements on the 'My Account' Page
        public IWebElement UsernameField => driver.FindElement(By.Id("username"));
        public IWebElement PasswordField => driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => driver.FindElement(By.Name("login"));
        public IWebElement ShopLink => driver.FindElement(By.LinkText("Shop"));
        public IWebElement LogoutButton => driver.FindElement(By.CssSelector("li[class='woocommerce-MyAccount-navigation-link woocommerce-MyAccount-navigation-link--customer-logout'] a"));
        public IWebElement OrderLink => driver.FindElement(By.CssSelector("li[class='woocommerce-MyAccount-navigation-link woocommerce-MyAccount-navigation-link--orders'] a"));


        //Procedures for the 'My Account' Page

        public void KeyIntoUsername(string keyData)
        {
            UsernameField.Clear();
            UsernameField.SendKeys(keyData);
        }

        public void KeyIntoPassword(string keyData)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(keyData);
        }

        public void ClickLogin()
        {
            LoginButton.Click();
        }
        public void ClickShopLink()
        {
            ShopLink.Click();
        }
        public void ClickLogout()
        {
            LogoutButton.Click();
        }
        public void ClickOrderLink()
        {
            OrderLink.Click();
        }

        //Advanced functions for 'MyAccount' page

        public void CompleteLogin(string username, string password)
        {
            KeyIntoUsername(username);
            KeyIntoPassword(password);
            LoginButton.Click();
        }
    }
}
