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
    public class LoginAndLogoutManagementStepDefinitions
    {
        [Given(@"I am on the my-account page")]
        public void GivenIAmOnTheMy_AccountPage()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
            driver.FindElement(By.LinkText("Dismiss")).Click();
            Console.WriteLine("Hello");
        }

        [When(@"I enter the username ""([^""]*)"" and the password ""([^""]*)""")]
        public void WhenIEnterTheUsernameAndThePassword(string username, string password)
        {
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            MyAccountPage.CompleteLogin("magmortar@pmail.com", "octoberComic0n!?");
        }

        [Then(@"I am logged in to my account")]
        public void ThenIAmLoggedInToMyAccount()
        {
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            bool LogoutPresent = MyAccountPage.LogoutButton.Displayed;
            Assert.That(LogoutPresent, Is.EqualTo(true), "Logout button is not present, user is not signed in");
            Console.WriteLine("Completed login process");
        }

        [Given(@"I am logged in to an account")]
        public void GivenIAmLoggedInToAnAccount()
        {
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            bool LogoutPresent = MyAccountPage.LogoutButton.Displayed;
            Assert.That(LogoutPresent, Is.EqualTo(true), "Logout button is not present, user is not signed in");
        }

        [When(@"I click logout")]
        public void WhenIClickLogout()
        {
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            MyAccountPage.LogoutButton.Click();
        }


        [Then(@"I am successfully logged out of my account")]
        public void ThenIAmSuccessfullyLoggedOutOfMyAccount()
        {
            MyAccountPage MyAccountPage = new MyAccountPage(driver);
            bool LoginPresent = MyAccountPage.LoginButton.Displayed;
            Assert.That(LoginPresent, Is.EqualTo(true), "Login button is not present, user is still signed in");
        }
    }
}
