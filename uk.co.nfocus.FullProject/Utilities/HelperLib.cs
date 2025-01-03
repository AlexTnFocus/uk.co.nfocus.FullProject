﻿using FullProject.POMs;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace uk.co.nfocus.FullProject.Utilities
{
    public static class HelperLib
    {
        public static void WaitForElementPresent(IWebDriver driver, By theElement)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(theElement).Displayed);
        }//Waits for an element to appear
        public static void WaitForElementEquals(IWebDriver driver, By theElement, string linkText)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(theElement).Text == linkText);
        }//Waits for an element's text value to match the expected value
        public static void WaitForElementNotPresent(IWebDriver driver, By theElement)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv =>
            {
                try
                {
                    return !drv.FindElement(theElement).Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }//Waits until an element is no longer present
        public static void WaitForElementNotEquals(IWebDriver driver, By theElement, string linkText)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(theElement).Text != linkText);
        }//Waits for an element's text value to not be equal to the expected result
        public static void WaitForElementClickable(IWebDriver driver, By theElement)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(theElement));
        }//Waits for an element to be clickable, incase it is temporarily disabled
        public static string GetMostRecentFolder(string parentFolder)
        {
            try
            {
                var directories = new DirectoryInfo(parentFolder).GetDirectories();
                var mostRecent = directories.OrderByDescending(d => d.CreationTime).FirstOrDefault();

                return mostRecent?.FullName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }//Retrieves the most recent folder created within a filepath
        public static void ScrollToTop(IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
        }//Scrolls to the top of the webpage
        public static decimal CalculateActualDiscount(string _subtotal, string _discount)
        {
            return (Decimal.Divide((Decimal.Parse(_subtotal) - Decimal.Parse(_discount)), Decimal.Parse(_subtotal)));
        }
        public static decimal CalculateTotal(string _subtotal, string _discount, string _shipping)
        {
            return Decimal.Parse(_subtotal) - Decimal.Parse(_discount) + Decimal.Parse(_shipping);
        }
        public static void ClearAndEnter(IWebElement _field, string _keyData)
        {
            _field.Clear();
            _field.SendKeys(_keyData);
        }
        public static void TakeScreenshot(IWebDriver driver, string screenshotName, string ssPath)
        {
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();

            string fullFilePath = Path.Combine(ssPath, screenshotName);
            screenshot.SaveAsFile(fullFilePath);
        }//Used to take screenshots to collect evidence
        public static void ClearBasketContents(IWebDriver driver)
        {
            NavigationPage NavigationPage = new NavigationPage(driver);
            NavigationPage.GoCart();
            CartPage CartPage = new CartPage(driver);
            CartPage.ResetCart();
        }

    }
}
