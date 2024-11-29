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
using uk.co.nfocus.FullProject.Support;
using TechTalk.SpecFlow.Assist;

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
        
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        private readonly WDWrapper _wdWrapper;
        private User _myUser;
        private LoginCredentials _creds;

        public ShopManagementStepDefinitions(WDWrapper wdWrapper, ScenarioContext scenarioContext, User MyUser, LoginCredentials MyCreds)
        {
            _scenarioContext = scenarioContext;
            _driver = wdWrapper.Driver;
            _myUser = MyUser;
            _creds = MyCreds;
        }

        [Given(@"I am logged in to an account using valid credentials")]
        public void GivenIAmLoggedInToMyAccount(Table table)
        {
            MyAccountPage MyAccountPage = new MyAccountPage(_driver);
            _myUser = table.CreateInstance<User>();
            MyAccountPage.CompleteLogin(_myUser.username, _myUser.password);
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
            ShopPage.AddItemToCart(item, CurrentCart);
            Console.WriteLine($"Added an item {item} to cart");
        }//Adds the passed item from the gherkin statement to the cart

        [Given(@"I am looking at the cart contents")]
        public void GivenIAmLookingAtTheCartContents()
        {
            NavigationPage NavigationPage = new NavigationPage(_driver);
            NavigationPage.GoCart();
            Console.WriteLine("Navigated to cart page");
        }//Navigates to the cart, which thanks to the waits earlier has the item that was added, in the cart

       // public string Coupon;//Used so the coupon used can be passed between multiple steps

        [When(@"I apply the coupon '([^']*)'")]
        public void WhenIApplyACoupon(string coupon) 
        {
            _scenarioContext["CouponCode"] = coupon;
            //Coupon = coupon;
            CartPage CartPage = new CartPage(_driver);
            CartPage.ResetCoupon(coupon);
            CartPage.KeyIntoCoupon(coupon);
            Console.WriteLine($"Typed in the coupon {coupon}");//Writes in the report of the coupon used
            CartPage.ClickCouponButton();
            Console.WriteLine("Clicked enter coupon button");
        }//Enters the coupon based on what is passed through from gherkin

        [Then(@"The coupon should take (.*)% off the price")]
        public void ThenTheCouponShouldDiscount(int discountPer)
        {
            String coupon = (String)_scenarioContext["CouponCode"];
            String basePath = (String)_scenarioContext["basePath"];
            CartPage CartPage = new CartPage(_driver);
            CartPage.WaitForProperTotal();
            TakeScreenshot(_driver, "TC1-1CouponVals.png", GetMostRecentFolder(basePath));
            //Takes a screenshot before the assertion so evidence is always availible
            TestContext.AddTestAttachment(GetMostRecentFolder(basePath) + @"\TC1-1CouponVals.png", "TC1-1");
            //Saves the screenshot in the folder created in the setup
            decimal discountReverse = Decimal.Divide(discountPer, 100);
            //Used so the assertion is easier to read
            decimal discountActual = 1 - CalculateActualDiscount(CartPage.GetSubtotal(), CartPage.GetDiscount(coupon));
            //Calculates the actual discount, decimals are used because exact values are important here and money calculations are involved
            Assert.That(discountActual, Is.EqualTo(discountReverse),
                $"Discount is not valid for {discountPer}% off, it is valid for {discountActual*100}% off");
            //Asserts whether the coupon is valid for the expected amount
            Console.WriteLine($"The coupon {coupon} is valid for {discountActual*100}% off");
        }

        [Then(@"The total after shipping should be correct")]
        public void ThenTheTotalAfterShippingShouldBeCorrect()
        {
            String basePath = (String)_scenarioContext["basePath"];
            String coupon = (String)_scenarioContext["CouponCode"];
            CartPage CartPage = new CartPage(_driver);
            Console.WriteLine($"The listed Total is {CartPage.GetTotal()}");
            Console.WriteLine($"The Subtotal is {CartPage.GetSubtotal()}");
            Console.WriteLine($"The Shipping is {CartPage.GetShipping()}");
            Console.WriteLine($"The Amount removed by the Discount is {CartPage.GetDiscount(coupon)}");
            //Writes all these values to the report
            TakeScreenshot(_driver, "TC1-2TotalShipping.png", GetMostRecentFolder(basePath));
            TestContext.AddTestAttachment(GetMostRecentFolder(basePath) + @"\TC1-2TotalShipping.png", "TC1-2");
            decimal calculatedTotal = CalculateTotal(CartPage.GetSubtotal(), CartPage.GetDiscount(coupon), CartPage.GetShipping());
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

        [Given(@"Placed the order using valid credentials")]
        public void GivenIHavePlacedTheOrder(Table table)
        {
            _creds = table.CreateInstance<LoginCredentials>();
            String basePath = (String)_scenarioContext["basePath"];
            CheckoutPage CheckoutPage = new CheckoutPage(_driver);
            CheckoutPage.EnterFullBilling(
                _creds.firstName, _creds.lastName, _creds.streetAddr,
                _creds.townCity, _creds.postcode, _creds.phone);//Enters billing details a la the EnterFullBilling function
            Console.WriteLine("Cleared existing billing and entered new details");
            CheckoutPage.ClickCheckPayment();
            Console.WriteLine("Clicked 'Check Payment'");
            CheckoutPage.ClickPlaceOrder();
            Console.WriteLine("Placed order");
            OrderRecievedPage OrderRecievedPage = new OrderRecievedPage(_driver);
            OrderRecievedPage.WaitForOrderNum();//Waits for the order number to appear
            OrderNumber = OrderRecievedPage.GetOrderNumber();
            Console.WriteLine($"Collected resulting order number {OrderNumber}");
            TakeScreenshot(_driver, "TC2-1CheckoutOrderNum.png", GetMostRecentFolder(basePath));
            TestContext.AddTestAttachment(GetMostRecentFolder(basePath) + @"\TC2-1CheckoutOrderNum.png", "TC2-1");
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
            String basePath = (String)_scenarioContext["basePath"];
            OrderPage OrderPage = new OrderPage(_driver);
            TakeScreenshot(_driver, "TC2-2OrdersOrderNum.png", GetMostRecentFolder(basePath));
            TestContext.AddTestAttachment(GetMostRecentFolder(basePath) + @"\TC2-2OrdersOrderNum.png", "TC2-2");
            Console.WriteLine($"The most recent order number is {OrderPage.GetTopOrderNum()}");
            Assert.That(OrderNumber, Is.EqualTo(OrderPage.GetTopOrderNum()), "Order numbers do not match");
            Console.WriteLine($"Confirmed order numbers {OrderNumber} and {OrderPage.GetTopOrderNum()} match");
        }//Confirms the order numbers match
        
    }
}
