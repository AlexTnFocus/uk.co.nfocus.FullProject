Feature: ShopManagementExtras

The user wants to be able to add items to their cart, apply coupons and place an order,
with the order being recorded and visible on their account. This test case exists to expand
on the core tests in ShopManagment.feature

Background: 
Given I am logged in to my account using 'magmortar@pmail.com' and 'octoberComic0n!?'

@Extra
Scenario Outline: Apply a coupon
	Given I have an item '<item>' in my cart
	And I am looking at the cart contents
	When I apply the coupon '<coupon>'
	Then The coupon should take <discount>% off the price
	And The total after shipping should be correct

Examples: 
| item   | coupon    | discount |
| Beanie | edgewords | 15       |
| Cap    | edgewords | 10       |
| Hoodie | nfocus    | 25       |
| Belt   | nfocus    | 12       |


#@Extra
#Scenario: Place an order
#	Given I have an item '<item>' in my cart
#	And I have proceeded to the checkout
#	And Placed the order
#	When I go to my-account/orders
#	Then The order number from the checkout should be listed

#Examples: 
#| item               |
#| Beanie             |
#| Belt               |
#| Cap                |
#| Hoodie             |
#| Hoodie with Logo   |
#| Hoodie with Pocket |
#| Hoodie with Zipper |
#| Long Sleeve Tee    |
#| Polo               |
#| Sunglasses         |
#| Tshirt             |
#| Vneck Tshirt       |