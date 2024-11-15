using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.Extensions;
using SeleniumExtras.WaitHelpers;

namespace uk.co.nfocus.FullProject.Utilities
{
    public static class HelperLib
    {
        public static void WaitForElementPresent(IWebDriver driver, By theElement)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(theElement).Displayed);
        }
        public static void WaitForElementEquals(IWebDriver driver, By theElement, string linkText)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(theElement).Text == linkText);
        }
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
        }
        public static void WaitForElementNotEquals(IWebDriver driver, By theElement, string linkText)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.FindElement(theElement).Text != linkText);
        }
        public static void WaitForElementClickable(IWebDriver driver, By theElement)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(theElement));
        }
        public static string GetMostRecentFolder(string parentFolder)
        {
            try
            {
                // Get all subdirectories in the specified folder
                var directories = new DirectoryInfo(parentFolder).GetDirectories();

                // Find the directory with the most recent creation time
                var mostRecent = directories.OrderByDescending(d => d.CreationTime).FirstOrDefault();

                return mostRecent?.FullName; // Return the full path of the most recent directory, or null if none found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
        public static void ScrollToTop(IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
        }

    }
}
