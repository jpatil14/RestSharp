Feature: PostProfile

@mytag
Scenario: Verify post operation 1
	Given I perform post operation "/api/users" with body
		| name  | job          |
		| jatin | test analyst |
	Then I should see the status code as "201"

Scenario: Verify post operation 2
	Given I perform post operation "/api/users" with body
		| name   | job             |
		| vishal | project manager |
	Then I should see the status code as "201"