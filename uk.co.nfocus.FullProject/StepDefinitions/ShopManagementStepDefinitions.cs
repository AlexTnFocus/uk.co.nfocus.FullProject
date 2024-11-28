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
using OpenQA.Selenium.DevTools.V128.Network;
using static System.Net.WebRequestMethods;
using Nfocus7Specflow.Support;

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
        private string couponPriv;
        private string orderNumberPriv;
        private string usernamePriv;
        private string passwordPriv;

        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        private readonly WDWrapper _wdWrapper;

        public ShopManagementStepDefinitions(WDWrapper wdWrapper, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = wdWrapper.Driver;

        }

        [Given(@"I am logged in to my account using '([^']*)' and '([^']*)'")]
        public void GivenIAmLoggedInToMyAccount(string username, string password)
        {
            MyAccountPage MyAccountPage = new MyAccountPage(_driver);
            MyAccountPage.CompleteLogin(username, password);
            try
            {
                Assert.That(MyAccountPage.LogoutButton, Is.Not.Null, "The logout button is not displayed, login failed.");
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail($"Logout button could not be accessed, the login attempt failed");
            }
            Console.WriteLine("Logged in");//consistent reporting helps indicate where failures occur in the report
            ClearBasketContents(_driver);
        }//Logs the user in

        [Given(@"I have added an item '([^']*)' to my cart")]
        public void GivenIHaveAnItemInMyCart(string item)
        {
            NavigationPage NavigationPage = new NavigationPage(_driver);
            ShopPage ShopPage = new ShopPage(_driver);
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
            NavigationPage NavigationPage = new NavigationPage(_driver);
            NavigationPage.GoCart();
            Console.WriteLine("Navigated to cart page");
        }//Navigates to the cart, which thanks to the waits earlier has the item that was added, in the cart

        public string Coupon;//Used so the coupon used can be passed between multiple steps

        [When(@"I apply the coupon '([^']*)'")]
        public void WhenIApplyACoupon(string coupon) 
        {
            Coupon = coupon;
            CartPage CartPage = new CartPage(_driver);
            CartPage.ResetCoupon(coupon);
            CartPage.KeyIntoCoupon(coupon);
            Console.WriteLine($"Typed in the coupon {Coupon}");//Writes in the report of the coupon used
            CartPage.ClickCouponButton();
            Console.WriteLine("Clicked enter coupon button");
        }//Enters the coupon based on what is passed through from gherkin

        [Then(@"The coupon should take (.*)% off the price")]
        public void ThenTheCouponShouldDiscount(int discountPer)
        {
            CartPage CartPage = new CartPage(_driver);
            CartPage.WaitForProperTotal();
            TakeScreenshot(_driver, "TC1-1CouponVals.png", GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\"));
            //Takes a screenshot before the assertion so evidence is always availible
            TestContext.AddTestAttachment(GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\") + @"\TC1-1CouponVals.png", "TC1-1");
            //Saves the screenshot in the folder created in the setup
            decimal discountReverse = Decimal.Divide(discountPer, 100);
            //Used so the assertion is easier to read
            decimal discountActual = 1 - CalculateActualDiscount(CartPage.GetSubtotal(), CartPage.GetDiscount(Coupon));
            //Calculates the actual discount, decimals are used because exact values are important here and money calculations are involved
            Assert.That(discountActual, Is.EqualTo(discountReverse),
                $"Discount is not valid for {discountPer}% off, it is valid for {discountActual*100}% off");
            //Asserts whether the coupon is valid for the expected amount
            Console.WriteLine($"The coupon {Coupon} is valid for {discountActual*100}% off");
        }

        [Then(@"The total after shipping should be correct")]
        public void ThenTheTotalAfterShippingShouldBeCorrect()
        {
            CartPage CartPage = new CartPage(_driver);
            Console.WriteLine($"The listed Total is {CartPage.GetTotal()}");
            Console.WriteLine($"The Subtotal is {CartPage.GetSubtotal()}");
            Console.WriteLine($"The Shipping is {CartPage.GetShipping()}");
            Console.WriteLine($"The Amount removed by the Discount is {CartPage.GetDiscount(Coupon)}");
            //Writes all these values to the report
            TakeScreenshot(_driver, "TC1-2TotalShipping.png", GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\"));
            TestContext.AddTestAttachment(GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\") + @"\TC1-2TotalShipping.png", "TC1-2");
            decimal calculatedTotal = CalculateTotal(CartPage.GetSubtotal(), CartPage.GetDiscount(Coupon), CartPage.GetShipping());
            Assert.That(calculatedTotal, Is.EqualTo(Decimal.Parse(CartPage.GetTotal())),
                $"Error, total after shipping is {calculatedTotal}, not {Decimal.Parse(CartPage.GetTotal())}");
            //Asserts the values presented produce the same result as the proposed one
            Console.WriteLine($"The total after shipping is calculated correctly," +
                $" the listed total is {CartPage.GetTotal()} and the actual total is {calculatedTotal}");
        }

        [Given(@"I have proceeded to the checkout")]
        public void GivenIHaveProceededToTheCheckout()
        {
            NavigationPage NavigationPage = new NavigationPage(_driver);
            NavigationPage.GoCheckout();
            Console.WriteLine("Navigated to checkout");
        }//Navigates to checkout

        public string OrderNumber;//Used to pass the ordernumber from placing the order between steps

        [Given(@"Placed the order")]
        public void GivenIHavePlacedTheOrder()
        {
            CheckoutPage CheckoutPage = new CheckoutPage(_driver);
            CheckoutPage.ClearFullBilling();//Clears the fields first as they can save old billing details
            CheckoutPage.EnterFullBilling();//Enters billing details a la the EnterFullBilling function
            Console.WriteLine("Cleared existing billing and entered new details");
            CheckoutPage.ClickCheckPayment();
            Console.WriteLine("Clicked 'Check Payment'");
            CheckoutPage.ClickPlaceOrder();
            Console.WriteLine("Placed order");
            OrderRecievedPage OrderRecievedPage = new OrderRecievedPage(_driver);
            OrderRecievedPage.WaitForOrderNum();//Waits for the order number to appear
            OrderNumber = OrderRecievedPage.GetOrderNumber();
            Console.WriteLine($"Collected resulting order number {OrderNumber}");
            TakeScreenshot(_driver, "TC2-1CheckoutOrderNum.png", GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\"));
            TestContext.AddTestAttachment(GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\") + @"\TC2-1CheckoutOrderNum.png", "TC2-1");
        }

        [When(@"I navigate to the orders page")]
        public void WhenIGoToTheOrdersPage()
        {
            NavigationPage NavigationPage = new NavigationPage(_driver);
            MyAccountPage MyAccountPage = new MyAccountPage(_driver);
            NavigationPage.GoMyAccount();
            Console.WriteLine("Navigated to My Account");
            MyAccountPage.ClickOrderLink();
            Console.WriteLine("Navigated to Orders");
        }//Navigates to the order page

        [Then(@"The order number from the checkout should be listed")]
        public void ThenTheOrderNumberFromCheckoutShouldBeListed()
        {
            OrderPage OrderPage = new OrderPage(_driver);
            TakeScreenshot(_driver, "TC2-2OrdersOrderNum.png", GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\"));
            TestContext.AddTestAttachment(GetMostRecentFolder(@"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots\") + @"\TC2-2OrdersOrderNum.png", "TC2-2");
            Console.WriteLine($"The most recent order number is {OrderPage.GetTopOrderNum()}");
            Assert.That(OrderNumber, Is.EqualTo(OrderPage.GetTopOrderNum()), "Order numbers do not match");
            Console.WriteLine($"Confirmed order numbers {OrderNumber} and {OrderPage.GetTopOrderNum()} match");
        }//Confirms the order numbers match
        
    }
}
