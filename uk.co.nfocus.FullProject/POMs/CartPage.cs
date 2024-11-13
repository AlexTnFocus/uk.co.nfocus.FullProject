using OpenQA.Selenium;
using static uk.co.nfocus.FullProject.Utilities.HelperLib;


namespace FullProject.POMs
{
    class CartPage
    {
        private IWebDriver driver;

        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators for elements on the 'Cart' Page
        public IWebElement CouponField => driver.FindElement(By.Id("coupon_code"));
        public IWebElement CouponButton => driver.FindElement(By.Name("apply_coupon"));

        
        //Procedures for the 'Shop' Page
        public void KeyIntoCoupon(string couponCode)
        {
            CouponField.Clear();
            CouponField.SendKeys(couponCode);
        }
        public void ClickCouponButton()
        {
            CouponButton.Click();
        }
        public string GetSubtotal() //add element locator up top, remove £ here
        {
            return driver.FindElement(By.CssSelector("tr[class='cart-subtotal'] bdi:nth-child(1)")).Text.Remove(0,1);
            
        }
        public string GetDiscount()
        {
            return driver.FindElement(By.CssSelector("td[data-title='Coupon: edgewords'] span[class='woocommerce-Price-amount amount']")).Text.Remove(0,1);
        }
        public string GetShipping()
        {
            return driver.FindElement(By.CssSelector("tr[class='woocommerce-shipping-totals shipping'] bdi:nth-child(1)")).Text.Remove(0, 1);
        }
        public string GetTotal()
        {
            return driver.FindElement(By.CssSelector("tr[class='order-total'] bdi:nth-child(1)")).Text.Remove(0, 1);
        }
        public void WaitForProperTotal()
        {
            WaitForElementPresent(driver, By.CssSelector("tr[class='cart-subtotal'] bdi:nth-child(1)"));
        }
        public void WaitForProperTotal2()
        {
            WaitForElementPresent(driver, By.CssSelector("tr[class='cart-discount coupon-edgewords'] th"));
        }
        public void ResetCoupon()
        {
            try
            {
                CouponField.Clear();
                driver.FindElement(By.CssSelector(".woocommerce-remove-coupon")).Click();
                WaitForElementEquals(driver, By.CssSelector("div[role='alert']"), "Coupon has been removed.");
                Console.WriteLine("Removed previously entered coupon");
                WaitForElementNotPresent(driver, By.CssSelector("tr[class='cart-discount coupon-edgewords'] th"));

            }catch (NoSuchElementException e)
            {
                Console.WriteLine("No coupon present, removing is unecessary");
            }

        }
    }
}
