Feature: Search Service

Background:
	Given I set the base URL as "http://asglwksdev-wssdv1/SearchService/json" with windows authentication

@mytag
Scenario:1 Remove Feature Demo Search Index
	Given I perform "POST" operation for endpoint "/RemoveSearchIndex"
	When I post below contents to remove the search index
		| team | indexId     |
		| WST  | FeatureDemo |
	Then I should see the status code as "200"

Scenario:2 Create Feature Demo Search Index
	Given I perform "POST" operation for endpoint "/AddSearchIndex"
	And I post the contents from template "FeatureDemoSearchIndex"
	Then I should see the status code as "201"

Scenario:3 Get Search Index
	Given I perform "GET" operation for endpoint "/SearchIndex/?team={teamName}&indexId={indexId}&userid={userid}"
	And I add below parameters in URL and execute the request
		| key      | value       |
		| teamName | WST         |
		| indexId  | FeatureDemo |
		| userid   | patilja     |
	Then I should see the status code as "200"

Scenario:4 Get Search Index Metadata
	Given I perform "GET" operation for endpoint "IndexMetadata/?team={teamName}"
	And I add below parameters in URL and execute the request
		| key      | value |
		| teamName | WST   |
	Then I should see the status code as "200"

Scenario:5 Merge Feature Demo Search Index
	Given I perform "POST" operation for endpoint "/MergeSearchIndex"
	And I post the contents from template "MergeFeatureDemoSearchIndex"
	Then I should see the status code as "200"

Scenario:6 Get Search Index Delta
	Given I perform "GET" operation for endpoint "SearchIndexDelta/?team={teamName}&indexId={indexId}&userid={userid}&revision={revision}"
	And I add below parameters in URL and execute the request
		| key      | value       |
		| teamName | WST         |
		| indexId  | FeatureDemo |
		| userid   | patilja     |
		| revision | 5           |
	Then I should see the status code as "200"

Scenario:7 Add Viewed Search Terms
	Given I perform "POST" operation for endpoint "/AddViewedSearchTerms"
	And I post the contents from template "AddViewedSearchItems"
	Then I should see the status code as "200"

Scenario:8 Get Most Viewed Search Terms
	Given I perform "GET" operation for endpoint "/MostViewedSearchTerms/?team={teamName}&maxTerms={maxTerms}&userid={userid}"
	And I add below parameters in URL and execute the request
		| key      | value   |
		| teamName | WST     |
		| maxTerms | 1       |
		| userid   | patilja |
	Then I should see the status code as "200"

Scenario:9 Get Recently Viewed Search Terms
	Given I perform "GET" operation for endpoint "/LastViewedSearchTerms/?team={teamName}&maxTerms={maxTerms}&userid={userid}"
	And I add below parameters in URL and execute the request
		| key      | value   |
		| teamName | WST     |
		| maxTerms | 5       |
		| userid   | patilja |
	Then I should see the status code as "200"

Scenario:10 Health Check Search service
	Given I perform "GET" operation for endpoint "/HealthCheck"
	When I execute the given "GET" request
	Then I should see the status code as "200"