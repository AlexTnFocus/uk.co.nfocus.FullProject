Feature: ShopManagement

The user wants to be able to add items to their cart, apply coupons and place an order,
with the order being recorded and visible on their account.

Background: 
Given I am logged in to my account using 'magmortar@pmail.com' and 'octoberComic0n!?'
And I have added an item 'Belt' to my cart

@Core
Scenario: Apply a coupon
	Given I am looking at the cart contents
	When I apply the coupon 'edgewords'
	Then The coupon should take 15% off the price
	And The total after shipping should be correct

@Core
Scenario: Place an order
	Given I have proceeded to the checkout
	And Placed the order
	When I navigate to the orders page
	Then The order number from the checkout should be listed

#@Core tag is used to indicate these tests were designed first and test what is desired
#only as much as needed, other tests test more