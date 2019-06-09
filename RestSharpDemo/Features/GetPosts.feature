Feature: GetPosts

@mytag
Scenario: Verify tha emailid of user having id as 2
	Given I perform GET operation for "api/users/{id}"
	And I perform operation for post "2"
	Then I should see the "email" as "janet.weaver@reqres.in"
