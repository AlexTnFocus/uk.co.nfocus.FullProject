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
        public IWebElement TopOrderNum => driver.FindElement(By.CssSelector("tbody tr:nth-child(1) td:nth-child(1) a:nth-child(1)"));
        
        //Functions for the 'Order' page
        public string GetTopOrderNum()//Gets the most recent order
        {
            var _topOrderNum = TopOrderNum.Text;
            return _topOrderNum.Remove(0, 1);
        }

    }
}
