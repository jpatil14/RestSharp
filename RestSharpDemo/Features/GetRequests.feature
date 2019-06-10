Feature: GetPosts

#Background: 
#Given I authenticate the user with following details
#		| userName | password     |
#		| testUser | testPassword |

@mytag
Scenario: Verify tha emailid of user having id as 2
	Given I perform GET operation for "api/users/{id}"
	And I perform operation for post "2"
	Then I should see the "email" as "janet.weaver@reqres.in"

Scenario: Verify tha emailid of user having id as 3
	Given I perform GET operation for "api/users/{id}"
	And I perform operation for post "3"
	Then I should see the "email" as "emma.wong@reqres.in"

Scenario: Verify tha emailid of user having id as 4
	Given I perform GET operation for "api/users/{id}"
	And I perform operation for post "4"
	Then I should see the "email" as "eve.holt@reqres.in"