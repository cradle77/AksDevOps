Feature: People endpoint

Scenario: 1 - Insert one person
	Given that the API is running
	When I execute a People POST request with "marco" and "marco@email.com"
	Then I get a 200 status code
	And a person called "marco" exists on the list 

Scenario: 2 - Get list of people
	Given that the API is running
	And there is a person called "carl" in the database
	And there is a person called "john" in the database
	When I execute a People GET request
	Then I get a 200 status code
	And there are at least 2 people
	And a person called "carl" exists on the list
	And a person called "john" exists on the list

Scenario: 3 - Delete one person
	Given that the API is running
	And there is a person called "jennifer" in the database
	When I execute a People DELETE request on its id
	Then I get a 200 status code
	And a person called "jennifer" does not exist on the list


