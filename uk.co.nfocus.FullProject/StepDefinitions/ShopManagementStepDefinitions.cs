using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using FluentAssertions;
using static uk.co.nfocus.FullProject.StepDefinitions.Hooks;
using System.Security.Cryptography.X509Certificates;
using FullProject.POMs;
using static OpenQA.Selenium.Screenshot;
using static uk.co.nfocus.FullProject.Utilities.HelperLib;

/*
Reporting:
    -All assertions have a screenshot associated with them, normally
    taken before the assertion happens so that evidence of what possibly
    failed is collected for reference.
    -Screenshots are included in the test report.
    -Progression through the test steps is documented in the test report,
    specifying variables used
 */

namespace uk.co.nfocus.FullProject.StepDefinitions
{
    [Binding]
    public class ShopManagementStepDefinitions
    {
        [Given(@"I am logged in to my account using '([^']*)' and '([^']*)'")]
        public void GivenIAmLoggedInToMyAccountUsingAnd(string username, string password)
        {
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
            Console.WriteLine("Launched Website");
            driver.FindElement(By.LinkText("Dismiss")).Click(); //Dismisses the demo banner as it gets in the way of some button presses
            Console.WriteLine("Dismissed bottom banner");
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            MyAccountPage.CompleteLogin(username, password);
            Console.WriteLine("Logged in");//consistent reporting helps indicate where failures occur in the report
        }//Logs the user in

        [Given(@"I have an item '([^']*)' in my cart")]
        public void GivenIHaveAnItemInMyCart(string item)
        {
            NavigationPage NavigationPage = new NavigationPage(driver);
            ShopPage ShopPage = new ShopPage(driver);
            NavigationPage.GoShop();
            Console.WriteLine("Navigated to shop page");
            string CurrentCart = ShopPage.GetCartContents();//Gets the number of items in the cart before another is added here
            ShopPage.AddItemToCart(item);
            Console.WriteLine($"Added an item {item} to cart");
            ShopPage.WaitForCartUpdate(CurrentCart);//Waits for the cart to update, otherwise the user could be met with an empty cart later
        }//Adds the passed item from the gherkin statement to the cart

        [Given(@"I am looking at the cart contents")]
        public void GivenIAmLookingAtTheCartContents()
        {
            NavigationPage NavigationPage = new NavigationPage(driver);
            NavigationPage.GoCart();
            Console.WriteLine("Navigated to cart page");
        }//Navigates to the cart, which thanks to the waits earlier has the item that was added, in the cart

        public string Coupon;//Used so the coupon used can be passed between multiple steps

        [When(@"I apply the coupon '([^']*)'")]
        public void WhenIApplyTheCoupon(string coupon)
        {
            Coupon = coupon;
            CartPage CartPage = new CartPage(driver);
            CartPage.ResetCoupon(coupon);
            CartPage.KeyIntoCoupon(coupon);
            Console.WriteLine($"Typed in the coupon {Coupon}");//Writes in the report of the coupon used
            CartPage.ClickCouponButton();
            Console.WriteLine("Clicked enter coupon button");
        }//Enters the coupon based on what is passed through from gherkin

        [Then(@"The coupon should take (.*)% off the price")]
        public void ThenTheCouponShouldTakeOffThePrice(int discountPer)
        {
            CartPage CartPage = new CartPage(driver);
            CartPage.WaitForProperTotal();
            TakeScreenshot(driver, "TC1-1CouponVals.png", GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\"));
            //Takes a screenshot before the assertion so evidence is always availible
            TestContext.AddTestAttachment(GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\") + @"\TC1-1CouponVals.png", "TC1-1");
            //Saves the screenshot in the folder created in the setup
            double discountReverse = 1-((double)discountPer / 100);
            //Used so the assertion is easier to read
            decimal discountActual = (Decimal.Divide((Decimal.Parse(CartPage.GetSubtotal()) - Decimal.Parse(CartPage.GetDiscount(Coupon))), Decimal.Parse(CartPage.GetSubtotal())));
            //Calculates the actual discount, decimals are used because exact values are important here and money calculations are involved
            Console.WriteLine(discountReverse);
            Assert.That(discountActual, Is.EqualTo(discountReverse),
                $"Discount is not valid for {discountPer}% off, it is valid for {(1 - discountActual)*100}% off");
            //Asserts whether the coupon is valid for the expected amount
            Console.WriteLine(CartPage.GetSubtotal().ToString());
            Console.WriteLine(CartPage.GetDiscount(Coupon).ToString());
        }

        [Then(@"The total after shipping should be correct")]
        public void ThenTheTotalAfterShippingShouldBeCorrect()
        {
            CartPage CartPage = new CartPage(driver);
            Console.WriteLine($"The listed Total is {CartPage.GetTotal()}");
            Console.WriteLine($"The Subtotal is {CartPage.GetSubtotal()}");
            Console.WriteLine($"The Shipping is {CartPage.GetShipping()}");
            Console.WriteLine($"The Discount is {CartPage.GetDiscount(Coupon)}");
            //Writes all these values to the report
            TakeScreenshot(driver, "TC1-2TotalShipping.png", GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\"));
            TestContext.AddTestAttachment(GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\") + @"\TC1-2TotalShipping.png", "TC1-2");
            Assert.That(Decimal.Parse(CartPage.GetSubtotal()) -
                Decimal.Parse(CartPage.GetDiscount(Coupon))
                + Decimal.Parse(CartPage.GetShipping()),
                Is.EqualTo(Decimal.Parse(CartPage.GetTotal())),
                "Error, total after shipping is incorrect");
            //Asserts the values presented produce the same result as the proposed one
            Console.WriteLine("The total after shipping is calculated correctly");
        }

        [Given(@"I have proceeded to the checkout")]
        public void GivenIHaveProceededToTheCheckout()
        {
            NavigationPage NavigationPage = new NavigationPage(driver);
            NavigationPage.GoCheckout();
            Console.WriteLine("Navigated to checkout");
        }//Navigates to checkout

        public string OrderNumber;//Used to pass the ordernumber from placing the order between steps

        [Given(@"Placed the order")]
        public void GivenPlacedTheOrder()
        {
            CheckoutPage CheckoutPage = new CheckoutPage(driver);
            CheckoutPage.ClearFullBilling();//Clears the fields first as they can save old billing details
            CheckoutPage.EnterFullBilling();//Enters billing details a la the EnterFullBilling function
            Console.WriteLine("Cleared existing billing and entered new details");
            CheckoutPage.ClickCheckPayment();
            Console.WriteLine("Clicked 'Check Payment'");
            CheckoutPage.ClickPlaceOrder();
            Console.WriteLine("Placed order");
            CheckoutPage.WaitForOrderNum();//Waits for the order number to appear
            OrderNumber = CheckoutPage.GetOrderNumber();
            Console.WriteLine($"Collected resulting order number {OrderNumber}");
            TakeScreenshot(driver, "TC2-1CheckoutOrderNum.png", GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\"));
            TestContext.AddTestAttachment(GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\") + @"\TC2-1CheckoutOrderNum.png", "TC2-1");
        }

        [When(@"I go to my-account/orders")]
        public void WhenIGoToMy_AccountOrders()
        {
            NavigationPage NavigationPage = new NavigationPage(driver);
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            NavigationPage.GoMyAccount();
            Console.WriteLine("Navigated to My Account");
            MyAccountPage.ClickOrderLink();
            Console.WriteLine("Navigated to Orders");
        }//Navigates to the order page

        [Then(@"The order number from the checkout should be listed")]
        public void ThenTheOrderNumberFromTheCheckoutShouldBeListed()
        {
            OrderPage OrderPage = new OrderPage(driver);
            TakeScreenshot(driver, "TC2-2OrdersOrderNum.png", GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\"));
            TestContext.AddTestAttachment(GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\") + @"\TC2-2OrdersOrderNum.png", "TC2-2");
            Assert.That(OrderNumber, Is.EqualTo(OrderPage.GetTopOrderNum()), "Order numbers do not match");
            Console.WriteLine($"Confirmed order numbers {OrderNumber} and {OrderPage.GetTopOrderNum()} match");
        }//Confirms the order numbers match
        public void TakeScreenshot(IWebDriver driver, string screenshotName, string ssPath)
        {
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();

            string fullFilePath = Path.Combine(ssPath, screenshotName);
            screenshot.SaveAsFile(fullFilePath);
        }//Used to take screenshots to collect evidence
    }
}
