Feature: Verify POST Operation

#Background:
#Given I authenticate the user with following details
#		| userName | password     |
#		| testUser | testPassword |
@mytag

Scenario Outline: Verify post operation with examples
	Given I perform "POST" operation for "/api/users"
	When I post the contents as '<name>''<job>'
	Then I should see the status code as "201"

	Examples:
		| name   | job          |
		| jatin  | test analyst |
		| Vishal | Manager      |