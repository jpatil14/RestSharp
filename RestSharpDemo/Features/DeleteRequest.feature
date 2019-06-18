Feature: Verify DELETE Operation

Background:
	Given I perform "DELETE" operation for "api/users/{id}"

@mytag
Scenario: Verify tha delete operation of user having id as 2
	When I retrieve the contents for resource "id" with value "2"
	Then I should see the status code as "204"

Scenario: Verify tha delete operation of user having id as 3
	When I retrieve the contents for resource "id" with value "3"
	Then I should see the status code as "204"

Scenario: Verify tha delete operation of user having id as 4
	When I retrieve the contents for resource "id" with value "4"
	Then I should see the status code as "204"