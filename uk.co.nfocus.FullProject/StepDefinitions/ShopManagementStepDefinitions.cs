using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using FluentAssertions;
using static uk.co.nfocus.FullProject.StepDefinitions.Hooks;
using System.Security.Cryptography.X509Certificates;
using FullProject.POMs;

namespace uk.co.nfocus.FullProject.StepDefinitions
{
    [Binding]
    public class ShopManagementStepDefinitions
    {
        [Given(@"I am logged in to my account")]
        public void GivenIAmLoggedInToMyAccount()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
            Console.WriteLine("Launched Website");
            driver.FindElement(By.LinkText("Dismiss")).Click(); //Make part of myaccountpage POM
            Console.WriteLine("Dismissed bottom banner");
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            MyAccountPage.CompleteLogin("magmortar@pmail.com", "octoberComic0n!?");
            Console.WriteLine("Logged in");
        }

        [Given(@"I have an item in my cart")]
        public void GivenIHaveAnItemInMyCart()
        {
            NavigationPage NavigationPage = new NavigationPage(driver);
            ShopPage ShopPage = new ShopPage(driver);
            NavigationPage.GoShop();
            Console.WriteLine("Navigated to shop page");
            ShopPage.AddItemToCart("Belt");
            Console.WriteLine("Added an item to cart");
        }

        [Given(@"I am looking at the cart contents")]
        public void GivenIAmLookingAtTheCartContents()
        {
            NavigationPage NavigationPage = new NavigationPage(driver);
            NavigationPage.GoCart();
            Console.WriteLine("Navigated to cart page");
        }

        [When(@"I apply the coupon edgewords")]
        public void WhenIApplyTheCouponEdgewords()
        {
            CartPage CartPage = new CartPage(driver);
            CartPage.ResetCoupon();
            CartPage.KeyIntoCoupon("edgewords");//Change to be passed through via gherkin
            Console.WriteLine("Typed in the coupon edgewords");
            CartPage.ClickCouponButton();
            Console.WriteLine("Clicked enter coupon button");
            //Possibly make this one function
        }

        [Then(@"The coupon should take (.*)% off the price")]
        public void ThenTheCouponShouldTakeOffThePrice(int discountPer)
        {
            CartPage CartPage = new CartPage(driver);
            CartPage.WaitForProperTotal2();
            Assert.That((Decimal.Divide((Decimal.Parse(CartPage.GetSubtotal()) - Decimal.Parse(CartPage.GetDiscount())), Decimal.Parse(CartPage.GetSubtotal()))),
                Is.EqualTo(0.85),
                "Discount is not valid for 15% off");
            Console.WriteLine(CartPage.GetSubtotal().ToString());
            Console.WriteLine(CartPage.GetDiscount().ToString());
        }

        [Then(@"The total after shipping should be correct")]
        public void ThenTheTotalAfterShippingShouldBeCorrect()
        {
            CartPage CartPage = new CartPage(driver);
            Assert.That(Decimal.Parse(CartPage.GetSubtotal()) -
                Decimal.Parse(CartPage.GetDiscount())
                + Decimal.Parse(CartPage.GetShipping()),
                Is.EqualTo(Decimal.Parse(CartPage.GetTotal())),
                "Error, total after shipping is incorrect");
            Console.WriteLine("Coupon is valid for 15% off");
            /*NavigationPage NavigationPage = new NavigationPage(driver);
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            NavigationPage.GoMyAccount();
            MyAccountPage.ClickLogout();//Add logout to teardown, not here*/
        }

        [Given(@"I have proceeded to the checkout")]
        public void GivenIHaveProceededToTheCheckout()
        {
            NavigationPage NavigationPage = new NavigationPage(driver);
            NavigationPage.GoCheckout();
            Console.WriteLine("Navigated to checkout");
        }

        public string OrderNumber;

        [Given(@"Placed the order")]
        public void GivenPlacedTheOrder()
        {
            CheckoutPage CheckoutPage = new CheckoutPage(driver);
            CheckoutPage.ClearFullBilling();
            CheckoutPage.EnterFullBilling();
            Console.WriteLine("Cleared existing billing and entered new details");
            CheckoutPage.ClickCheckPayment();
            Console.WriteLine("Clicked 'Check Payment'");
            CheckoutPage.ClickPlaceOrder();
            Console.WriteLine("Placed order");
            CheckoutPage.WaitForOrderNum();
            OrderNumber = CheckoutPage.GetOrderNumber();
            Console.WriteLine($"Collected resulting order number {OrderNumber}");
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
        }

        [Then(@"The order number from the checkout should be listed")]
        public void ThenTheOrderNumberFromTheCheckoutShouldBeListed()
        {
            OrderPage OrderPage = new OrderPage(driver);
            Assert.That(OrderNumber, Is.EqualTo(OrderPage.GetTopOrderNum()), "Order numbers do not match");
            Console.WriteLine($"Confirmed order numbers {OrderNumber} and {OrderPage.GetTopOrderNum()} match");
        }
    }
}
