Feature: Create Book Order
  As a registered API client
  I want to create and manage book orders
  So that I can purchase books from the simple-books-api

@smoke @positive @order
Scenario: Successfully create a book order with valid data
	Given API endpoint is available "/orders"
	And I have the following book order details:
		| Field        | Value    |
		| bookId       |        1 |
		| customerName | John Doe |
	When I submit a POST request to create the "/orders"
	Then the response status code should be 201
	And the response should contain an "orderId"
	And the order should be created successfully

@positive @order
Scenario: Create order with valid bookId and long customer name
	Given I have a book with ID 2
	And I have a customer name "Alexander Maximilian Christopher"
	When I submit a POST request to create the order
	Then the response status code should be 201
	And the response should contain a valid order ID

@positive @order
Scenario: Retrieve order details after creation
	Given I have created an order for book ID 3 with customer "Jane Smith"
	When I submit a GET request to retrieve the order
	Then the response status code should be 200
	And the order details should match:
		| Field        | Value      |
		| bookId       |          3 |
		| customerName | Jane Smith |

@negative @order @validation
Scenario: Cannot create order with missing bookId
	Given I have the following incomplete order details:
		| Field        | Value    |
		| customerName | John Doe |
	When I submit a POST request to create the order
	Then the response status code should be 400
	And the error message should contain "bookId is required"

@negative @order @validation
Scenario: Cannot create order with missing customerName
	Given I have the following incomplete order details:
		| Field  | Value |
		| bookId |     1 |
	When I submit a POST request to create the order
	Then the response status code should be 400
	And the error message should contain "customerName is required"

@negative @order @validation
Scenario: Cannot create order with invalid bookId
	Given I have a book with ID 99999
	And I have a customer name "Test User"
	When I submit a POST request to create the order
	Then the response status code should be 404
	And the error message should contain "book not found"

@negative @order @validation
Scenario Outline: Cannot create order with invalid data types
	Given I have the following order details with invalid data:
		| Field        | Value           |
		| bookId       | <invalidBookId> |
		| customerName | <customerName>  |
	When I submit a POST request to create the order
	Then the response status code should be <statusCode>
	And the error message should contain "<errorMessage>"

Examples:
	| invalidBookId | customerName | statusCode | errorMessage             |
	| abc           | John Doe     |        400 | Invalid bookId format    |
	|            -1 | John Doe     |        400 | bookId must be positive  |
	|             1 |              |        400 | customerName is required |
	|             1 | null         |        400 | customerName is required |

@negative @order @authorization
Scenario: Cannot create order without authentication token
	Given I do not have an authentication token
	And I have the following book order details:
		| Field        | Value    |
		| bookId       |        1 |
		| customerName | John Doe |
	When I submit a POST request to create the order
	Then the response status code should be 401
	And the error message should contain "Unauthorized"

@negative @order @authorization
Scenario: Cannot create order with invalid authentication token
	Given I have an invalid authentication token "invalid-token-12345"
	And I have the following book order details:
		| Field        | Value    |
		| bookId       |        1 |
		| customerName | John Doe |
	When I submit a POST request to create the order
	Then the response status code should be 401
	And the error message should contain "Invalid token"

@positive @order @update
Scenario: Successfully update an existing order
	Given I have created an order for book ID 1 with customer "John Doe"
	When I submit a PATCH request to update the customer name to "Jane Doe"
	Then the response status code should be 204
	And the order should be updated successfully

@positive @order @delete
Scenario: Successfully delete an existing order
	Given I have created an order for book ID 1 with customer "Test User"
	When I submit a DELETE request to remove the order
	Then the response status code should be 204
	And the order should no longer exist

@negative @order @delete
Scenario: Cannot delete a non-existent order
	Given I have an order ID "non-existent-order-id"
	When I submit a DELETE request to remove the order
	Then the response status code should be 404
	And the error message should contain "Order not found"

@performance @order
Scenario: API responds within acceptable time for order creation
	Given I have the following book order details:
		| Field        | Value    |
		| bookId       |        1 |
		| customerName | John Doe |
	When I submit a POST request to create the order
	Then the response status code should be 201
	And the response time should be less than 2000 milliseconds
