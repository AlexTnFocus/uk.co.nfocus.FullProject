using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullProject.POMs;

namespace uk.co.nfocus.FullProject.StepDefinitions
{
    [Binding]
    public partial class Hooks
    {

        public static IWebDriver driver; //field to share driver between class methods

        [Before] //Similar to NUnit [SetUp]
        public static void SetUp()
        {
            driver = new ChromeDriver();
        }
        [After] //Similar to NUnit [TearDown]
        public static void TearDown()
        {
            NavigationPage NavigationPage = new NavigationPage(driver);
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            Console.WriteLine("Test objective complete, logging out");
            NavigationPage.GoMyAccount();
            Console.WriteLine("Navigated to My Account");
            MyAccountPage.ClickLogout();
            Console.WriteLine("Logged out");
            driver.Quit();
        }
    }
}
