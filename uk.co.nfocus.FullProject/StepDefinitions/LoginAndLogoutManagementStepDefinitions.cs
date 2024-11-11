using System;
using TechTalk.SpecFlow;

namespace uk.co.nfocus.FullProject.StepDefinitions
{
    [Binding]
    public class LoginAndLogoutManagementStepDefinitions
    {
        [Given(@"I am on the ""([^""]*)"" page")]
        public void GivenIAmOnThePage(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"I enter the username ""([^""]*)"" and the password ""([^""]*)""")]
        public void WhenIEnterTheUsernameAndThePassword(string p0, string p1)
        {
            throw new PendingStepException();
        }

        [Then(@"I am logged in to my account")]
        public void ThenIAmLoggedInToMyAccount()
        {
            throw new PendingStepException();
        }

        [Given(@"I am logged in to an account")]
        public void GivenIAmLoggedInToAnAccount()
        {
            throw new PendingStepException();
        }

        [When(@"I click ""([^""]*)""")]
        public void WhenIClick(string logout)
        {
            throw new PendingStepException();
        }

        [Then(@"I am successfully logged out of my account")]
        public void ThenIAmSuccessfullyLoggedOutOfMyAccount()
        {
            throw new PendingStepException();
        }
    }
}
