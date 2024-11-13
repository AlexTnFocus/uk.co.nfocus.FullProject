//Make a completed order page POM
//GetOrderNumber should be a locator
//EnterFullBilling pass values not hard coded

using OpenQA.Selenium;
using static uk.co.nfocus.FullProject.Utilities.HelperLib;


namespace FullProject.POMs
{
    class CheckoutPage
    {
        private IWebDriver driver;

        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators for elements on the 'Checkout' page
        public IWebElement FirstNameField => driver.FindElement(By.CssSelector("#billing_first_name"));
        public IWebElement LastNameField => driver.FindElement(By.CssSelector("#billing_last_name"));
        public IWebElement BillAddOne =>  driver.FindElement(By.CssSelector("#billing_address_1"));
        public IWebElement BillCity =>  driver.FindElement(By.CssSelector("#billing_city"));
        public IWebElement BillPostcode =>  driver.FindElement(By.CssSelector("#billing_postcode"));
        public IWebElement BillPhone =>  driver.FindElement(By.CssSelector("#billing_phone"));
        public IWebElement CheckPaymentButton => driver.FindElement(By.CssSelector("label[for= 'payment_method_cheque']"));
        public IWebElement PlaceOrderButton => driver.FindElement(By.CssSelector("#place_order"));
        public IWebElement MyAccountLink => driver.FindElement(By.CssSelector("li[id='menu-item-46'] a"));
        public IWebElement CheckoutOrderNum => driver.FindElement(By.CssSelector("li[class='woocommerce-order-overview__order order'] strong"));

        //Functions for the 'Checkout' page
        public void KeyIntoFNF(string keyData)
        {
            FirstNameField.SendKeys(keyData);
        }
        public void KeyIntoLNF(string keyData)
        {
            LastNameField.SendKeys(keyData);
        }
        public void KeyIntoBAO(string keyData)
        {
            BillAddOne.SendKeys(keyData);
        }
        public void KeyIntoBC(string keyData)
        {
            BillCity.SendKeys(keyData);
        }
        public void KeyIntoBPost(string keyData)
        {
            BillPostcode.SendKeys(keyData);
        }
        public void KeyIntoBPhone(string keyData)
        {
            BillPhone.SendKeys(keyData);
        }
        public void ClickCheckPayment()
        {
            CheckPaymentButton.Click();
        }
        public void ClickPlaceOrder()
        {
            PlaceOrderButton.Click();
        }
        public string GetOrderNumber()
        {
            return driver.FindElement(By.CssSelector("li[class='woocommerce-order-overview__order order'] strong")).Text;
        }
        public void ClickMyAccountLink()
        {
            MyAccountLink.Click();
        }

        //Advanced functions for 'Checkout' page
        public void EnterFullBilling()
        {
            KeyIntoFNF("John");
            KeyIntoLNF("Nameson");
            KeyIntoBAO("1 Streetsville");
            KeyIntoBC("Citytown");
            KeyIntoBPost("SA44 4NE");
            KeyIntoBPhone("01234567890");
        }

        public void ClearFullBilling()
        {
            FirstNameField.Clear();
            LastNameField.Clear();
            BillAddOne.Clear();
            BillCity.Clear();
            BillPostcode.Clear();
            BillPhone.Clear();
        }
        public void WaitForOrderNum()
        {
            WaitForElementPresent(driver, By.CssSelector("li[class='woocommerce-order-overview__order order'] strong"));
        }

    }
}
