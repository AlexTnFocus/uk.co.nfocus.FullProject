//MyAccountPage shorten css selectors if possible, privatise locators

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.FullProject.Utilities.HelperLib;

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
        public void ClickLogin()//Clicks the login button
        {
            LoginButton.Click();
        }
        public void ClickLogout()//Clicks the logout button
        {
            LogoutButton.Click();
        }
        public void ClickOrderLink()//Clicks the order link
        {
            OrderLink.Click();
        }

        //Advanced functions for 'MyAccount' page

        public void CompleteLogin(string username, string password)//Performs a full login
        {
            ClearAndEnter(UsernameField, username);
            ClearAndEnter(PasswordField, password);
            LoginButton.Click();
        }
    }
}
