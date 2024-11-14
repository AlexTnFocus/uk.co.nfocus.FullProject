Feature: ShopManagement

A short summary of the feature

Background: 
Given I am logged in to my account using 'magmortar@pmail.com' and 'octoberComic0n!?'
And I have an item 'Belt' in my cart
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