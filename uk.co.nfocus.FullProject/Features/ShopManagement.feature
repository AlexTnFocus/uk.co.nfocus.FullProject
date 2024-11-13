Feature: ShopManagement

A short summary of the feature

Background: 
Given I am logged in to my account
And I have an item in my cart
#Possible to use Examples to test with adding different items to the cart

Scenario: Apply a coupon
	Given I am looking at the cart contents
	When I apply the coupon edgewords
	Then The coupon should take 15% off the price
	And The total after shipping should be correct

Scenario: Place an order
	Given I have proceeded to the checkout
	And Placed the order
	When I go to my-account/orders
	Then The order number from the checkout should be listed

#Scenario: TestCase1
#	Given I am on the my-account page
#	When I login
#	And I go to the shop page
#	And I add an item to the cart
#	And I view the cart
#	And I apply a coupon
#	Then The coupon should take 15% off the price
#	And The total after shipping should be correct
#	When I click logout
#	Then I am successfully logged out of my account

#Scenario: TestCase2
#	Given I am on the my-account page
#	When I login
#	And I go to the shop page
#	And I add an item to the cart
#	And I view the cart
#	And I Proceed to checkout
#	And Complete billing details
#	And select check payment
#	And I place the order
#	And Capture the order number
#	And go to account->orders
#	Then the same order number should be present
