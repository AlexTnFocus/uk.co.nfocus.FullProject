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
        public IWebElement Subtotal => driver.FindElement(By.CssSelector("tr[class='cart-subtotal'] bdi:nth-child(1)"));
        public IWebElement Shipping => driver.FindElement(By.CssSelector("tr[class='woocommerce-shipping-totals shipping'] bdi:nth-child(1)"));
        public IWebElement Total => driver.FindElement(By.CssSelector("tr[class='order-total'] bdi:nth-child(1)"));
        public IWebElement RemoveCoupon => driver.FindElement(By.CssSelector(".woocommerce-remove-coupon"));
        public IWebElement RemoveItem => driver.FindElement(By.CssSelector("tbody tr:nth-child(1) td:nth-child(1) a:nth-child(1)"));

        //Procedures for the 'Shop' Page
        public void KeyIntoCoupon(string couponCode)
        {
            ClearAndEnter(CouponField, couponCode);
        }
        public void ClickCouponButton()
        {
            CouponButton.Click();
        }
        public string GetSubtotal()//Gets the subtotal and removes the £ sign for calculation purposes
        {
            //return driver.FindElement(By.CssSelector("tr[class='cart-subtotal'] bdi:nth-child(1)")).Text.Remove(0,1);
            return Subtotal.Text.Remove(0, 1);

        }
        public string GetDiscount(string coupon)//Gets the price removed by the coupon, and removes the £ sign
        {
            return driver.FindElement(By.CssSelector($"td[data-title='Coupon: {coupon}'] span[class='woocommerce-Price-amount amount']")).Text.Remove(0,1);
        }
        public string GetShipping()//Gets the shipping price and removes the £ sign
        {
            return Shipping.Text.Remove(0, 1);
        }
        public string GetTotal()//Gets the total price and removes the £ sign
        {
            return Total.Text.Remove(0, 1);
        }
        public void WaitForProperTotal()//Waits for the total to update after the coupon is applied
        {
            WaitForElementPresent(driver, By.CssSelector(".woocommerce-remove-coupon"));
        }
        public void ResetCoupon(string coupon)//Removes any previously applied coupons
        {
            try
            {
                CouponField.Clear();
                RemoveCoupon.Click();
                WaitForElementEquals(driver, By.CssSelector("div[role='alert']"), "Coupon has been removed.");
                Console.WriteLine("Removed previously entered coupon");
                WaitForElementNotPresent(driver, By.CssSelector($"tr[class='cart-discount coupon-{coupon}'] th"));

            }catch (NoSuchElementException e)
            {
                Console.WriteLine("No coupon present, removing is unecessary");
            }

        }
        public void ResetCart()
        {
            try
            {
                RemoveItem.Click();
                Thread.Sleep(1000);
                ResetCart();
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Cart has been cleared");
            }
        }
    }
}
