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

    }
}
