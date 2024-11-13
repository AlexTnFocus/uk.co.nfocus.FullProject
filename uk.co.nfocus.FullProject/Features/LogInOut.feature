Feature: Login and Logout Management
		To be able to access their accounts and keep them secure,
		users must be able to login and logout from their accounts

Background: 
Given I am on the my-account page

Scenario: Logging in with valid credentials
	When I enter the username "magmortar@pmail.com" and the password "octoberComic0n!?"
	Then I am logged in to my account

Scenario: Logging out of an account
	Given I am logged in to an account
	When I click logout
	Then I am successfully logged out of my account
