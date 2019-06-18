Feature: Verify PUT Operation

#Background:
#Given I authenticate the user with following details
#		| userName | password     |
#		| testUser | testPassword |
@mytag

Scenario: Verify put operation
	Given I perform "PUT" operation for "/api/users/{id}"
	When I update the below contents for "id" as "2"
		| name   | job     |
		| Vishal | Manager |
	Then I should see the status code as "200"

