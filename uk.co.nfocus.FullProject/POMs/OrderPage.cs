//use locator for GetTopOrderNum, not method

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullProject.POMs
{
    class OrderPage
    {
        private IWebDriver driver;

        public OrderPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators for elements on the 'Order' page
        public IWebElement MyAccountLink => driver.FindElement(By.CssSelector("li[id='menu-item-46'] a"));

        //Functions for the 'Order' page
        public string GetTopOrderNum()
        {
            var TopOrderNum = driver.FindElement(By.CssSelector("tbody tr:nth-child(1) td:nth-child(1) a:nth-child(1)")).Text;
            return TopOrderNum.Remove(0, 1);
        }
        public void ClickMyAccountLink()
        {
            MyAccountLink.Click();
        }

    }
}
