Feature: Verify GET Operation

#Background:
#Given I authenticate the user with following details
#		| userName | password     |
#		| testUser | testPassword |
@mytag
Scenario: Verify tha emailid of user having id as 2
	Given I perform "GET" operation for "/api/users?page={pageNo}"
	When I retrieve the contents for resource "pageNo" with value "2"
	Then I should see the "total_pages" as "4"
	And I should see first_name as "Charles" for id as "5"

Scenario: Verify tha emailid of user having id as 4
	Given I perform "GET" operation for "api/users/{id}"
	When I retrieve the contents for resource "id" with value "4"
	Then I should see below values in response body of data resource
		| id | email              | first_name | last_name | avatar                                                              |
		| 4  | eve.holt@reqres.in | Eve        | Holt      | https://s3.amazonaws.com/uifaces/faces/twitter/marcoramires/128.jpg |

Scenario: Verify tha emailid of user having id as 5
	Given I perform "GET" operation for "api/users/{id}"
	When I retrieve the contents for resource "id" with value "5"
	Then I should see below values in response body of data resource
		| id | email                    | first_name | last_name | avatar                                                             |
		| 5  | charles.morris@reqres.in | Charles    | Morris    | https://s3.amazonaws.com/uifaces/faces/twitter/stephenmoon/128.jpg |