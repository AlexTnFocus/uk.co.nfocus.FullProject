using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.FullProject.Utilities.HelperLib;

namespace FullProject.POMs
{
    class OrderRecievedPage
    {
        private IWebDriver driver;

        public OrderRecievedPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators for elements on the 'Order Recieved' page

        public IWebElement OrderNumber => driver.FindElement(By.CssSelector("li[class='woocommerce-order-overview__order order'] strong"));

        //Functions for the 'Order' page
        public string GetOrderNumber()//Returns the order number
        {
            return OrderNumber.Text;
        }
        public void WaitForOrderNum()//Waits for the order number to appear
        {
            WaitForElementPresent(driver, By.CssSelector("li[class='woocommerce-order-overview__order order'] strong"));
        }

    }
}
