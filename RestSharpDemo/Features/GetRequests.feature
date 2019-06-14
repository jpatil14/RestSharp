Feature: GetPosts

#Background:
#Given I authenticate the user with following details
#		| userName | password     |
#		| testUser | testPassword |
@mytag
Scenario: Verify tha emailid of user having id as 2
	Given I perform "GET" operation for "/api/users?page={id}"
	When I retrieve the contents for resource "id" with value "2"
	Then I should see the "email" as "eve.holt@reqres.in"

Scenario: Verify tha emailid of user having id as 3
	Given I perform "GET" operation for "api/users/{id}"
	When I retrieve the contents for resource "id" with value "3"
	Then I should see the "email" as "emma.wong@reqres.in"

Scenario: Verify tha emailid of user having id as 4
	Given I perform "GET" operation for "api/users/{id}"
	When I retrieve the contents for resource "id" with value "4"
	Then I should see the "email" as "eve.holt@reqres.in"

Scenario: Verify put operation
	Given I perform "PUT" operation for "/api/users/{id}"
	When I update the below contents for "id" as "2"
		| name   | job     |
		| Vishal | Manager |
	Then I should see the status code as "200"

Scenario Outline: Verify post operation with examples
	Given I perform "POST" operation for "/api/users"
	When I post the contents as '<name>''<job>'
	Then I should see the status code as "201"

	Examples:
		| name   | job          |
		| jatin  | test analyst |
		| Vishal | Manager      |