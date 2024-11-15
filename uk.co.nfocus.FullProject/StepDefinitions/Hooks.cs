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
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using OpenQA.Selenium.Interactions;
using static OpenQA.Selenium.BiDi.Modules.Script.RealmInfo;
using ServiceStack;
using static uk.co.nfocus.FullProject.Utilities.HelperLib;
using System.Drawing;

namespace uk.co.nfocus.FullProject.StepDefinitions
{
    [Binding]
    public partial class Hooks
    {
        public string filePath;

        public static IWebDriver driver; //field to share driver between class methods

        [Before] //Similar to NUnit [SetUp]
        public static void SetUp()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);

            //Creates a folder for the screenshots
            string basePath = @"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots";
            string folderName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"); // Format: YYYY-MM-DD_HH-MM-SS
            string fullPath = Path.Combine(basePath, folderName);
            Directory.CreateDirectory(fullPath);
        }
        [After] //Similar to NUnit [TearDown]
        public static void TearDown()
        {
            Console.WriteLine("Test objective complete, logging out");
            Thread.Sleep(500);
            NavigationPage NavigationPage = new NavigationPage(driver);
            Actions Actions = new Actions(driver);
            Actions.MoveToElement(NavigationPage.MyAccount);
            

            //ScrollToTop(driver);

            //ScrollToTop(driver);
            NavigationPage.GoMyAccount();//Watch this closely, has failed to execute before
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            Console.WriteLine("Navigated to My Account");          
            MyAccountPage.ClickLogout();
            Console.WriteLine("Logged out");
            driver.Quit();
        }
    }
}
