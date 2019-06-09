Feature: PostProfile
	

@mytag
Scenario: Verify post operation
	Given I perform post operation "/api/users" with body
		| name | job |
		| jatin     |test analyst     |
	Then I should see "name" as "jatin"
