using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using FullProject.POMs;
using OpenQA.Selenium.Interactions;


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
            options.AddArgument("--start-maximized");//Opens the webpage fullscreen
            driver = new ChromeDriver(options);//Creates the driver

            //Creates a folder for the screenshots
            string basePath = @"C:\Users\AlexTongue\OneDrive - nFocus Limited\Pictures\Test Screenshots";//Will need changing, look into getContext
            string folderName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"); // Format: YYYY-MM-DD_HH-MM-SS
            string fullPath = Path.Combine(basePath, folderName);
            Directory.CreateDirectory(fullPath);//Creates the folder for saving screenshots in
        }
        [After] //Similar to NUnit [TearDown]
        public static void TearDown()
        {
            Console.WriteLine("Test objective complete, logging out");
            Thread.Sleep(500);//Needs changing, however is necessary for certain execution of TC1
            NavigationPage NavigationPage = new NavigationPage(driver);
            Actions Actions = new Actions(driver);
            Actions.MoveToElement(NavigationPage.MyAccount);//Moves focus to the My Account button 
            NavigationPage.GoMyAccount();//Watch this closely, has failed to execute before
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            Console.WriteLine("Navigated to My Account");          
            MyAccountPage.ClickLogout();//Clicks the logout button
            Console.WriteLine("Logged out");
            driver.Quit();//Closes the driver 
        }
    }
}
