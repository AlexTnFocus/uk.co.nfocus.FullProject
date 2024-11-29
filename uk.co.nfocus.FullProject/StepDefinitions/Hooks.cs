using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullProject.POMs;
using Nfocus7Specflow.Support;
using static uk.co.nfocus.FullProject.Utilities.HelperLib;

namespace uk.co.nfocus.FullProject.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        public string filePath;

        //public static IWebDriver driver; //field to share driver between class methods

        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private IWebDriver _driver; //field to share driver between class methods
        private readonly WDWrapper _wdWrapper;
        public Hooks(WDWrapper wdWrapper, FeatureContext featureContext, ScenarioContext scenarioContext) //Constructor will be run by Specflow when it instantiates this class to use the [Before] step. When it does that it will create a ScenarioCOntext object. Other step classes that need a scenario context in their constructor will use the same ScenarioContext instance.
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _wdWrapper = wdWrapper; //WDWrapper will be instanticated and passed in by Specflow
        }

        [Before] //Similar to NUnit [SetUp]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");//Opens the webpage fullscreen
            _driver = new ChromeDriver(options);//Creates the driver

            //Creates a folder for the screenshots, will work on any machine now. Saved in bin/debug/net6.0/
            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            // Get the directory path containing the assembly
            string basePath = Path.GetDirectoryName(assemblyPath);
            string folderName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"); // Format: YYYY-MM-DD_HH-MM-SS
            string fullPath = Path.Combine(basePath, folderName);
            Console.WriteLine(basePath);
            _scenarioContext["basePath"] = basePath;
            Directory.CreateDirectory(fullPath);//Creates the folder for saving screenshots in
            _driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
            Console.WriteLine("Launched Website");
            NavigationPage NavigationPage = new NavigationPage(_driver);
            NavigationPage.ClickDismiss(); //Dismisses the demo banner as it gets in the way of some button presses
            Console.WriteLine("Dismissed bottom banner");

            _wdWrapper.Driver = _driver;
            _scenarioContext["alsowebdriver"] = _driver;

        }
        [After] //Similar to NUnit [TearDown]
        public void TearDown()
        {
            try
            {
                Console.WriteLine("Test objective complete, logging out");
                Thread.Sleep(500);//Needs changing, however is necessary in case the cart is empty.
                                  //Some unseen and unknown load happens that takes time but
                                  //must be wait out for execution to continue properly
                NavigationPage NavigationPage = new NavigationPage(_driver);
                NavigationPage.GoMyAccount();//Watch this closely, has failed to execute before
                MyAccountPage MyAccountPage = new MyAccountPage(_driver);
                Console.WriteLine("Navigated to My Account");
                MyAccountPage.ClickLogout();//Clicks the logout button
                Console.WriteLine("Logged out");
            }
            finally
            {
                _driver.Quit();//Closes the driver 
            }
        }
    }
}
