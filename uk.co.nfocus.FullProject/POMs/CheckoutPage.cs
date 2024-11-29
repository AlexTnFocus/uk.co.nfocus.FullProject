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
        public void KeyIntoFirstNameField(string keyData)//Writes into the first name field
        {
            FirstNameField.SendKeys(keyData);
        }
        public void KeyIntoLastNameField(string keyData)//Writes into the last name field
        {
            LastNameField.SendKeys(keyData);
        }
        public void KeyIntoBillAddrOne(string keyData)//Writes into the street address field
        {
            BillAddOne.SendKeys(keyData);
        }
        public void KeyIntoBillCity(string keyData)//Writes into the town/city field
        {
            BillCity.SendKeys(keyData);
        }
        public void KeyIntoBillPostcode(string keyData)//Writes into the postcode field
        {
            BillPostcode.SendKeys(keyData);
        }
        public void KeyIntoBillPhone(string keyData)//Writes into the phone field
        {
            BillPhone.SendKeys(keyData);
        }
        public void ClickCheckPayment()//Clicks the check payment for payment option
        {
            CheckPaymentButton.Click();
        }
        public void ClickPlaceOrder()//Clicks the place order button
        {
            PlaceOrderButton.Click();
        }

        //Advanced functions for 'Checkout' page
        public void EnterFullBilling(
            string firstName, string lastName, string streetAddr,
            string townCity, string postcode, string phone)//Enters these preset billing details
        {
            ClearAndEnter(FirstNameField, firstName);
            ClearAndEnter(LastNameField, lastName);
            ClearAndEnter(BillAddOne, streetAddr);
            ClearAndEnter(BillCity, townCity);
            ClearAndEnter(BillPostcode, postcode);
            ClearAndEnter(BillPhone, phone);
        }
    }
}
