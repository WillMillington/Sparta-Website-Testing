﻿Feature: NoAccessTests
	In order to avoid breach of security
	As a developer
	I want to ensure that no webpage can be accessed without a valid login

@NoTokenNoAccess
Scenario: Try to access results page with no login
	When I type in the results page url
	Then I should be on the login page
	And I should see an error message "Error: No token provided, please login to access this page!"

@NoTokenNoAccess
Scenario: Try to access dispatches page with no login
	When I type in the dispatches page url
	Then I should be on the login page
	And I should see an error message "Error: No token provided, please login to access this page!"

@NoTokenNoAccess
Scenario: Try to access home page with no login
	When I type in the home page url
	Then I should be on the login page
	And I should see an error message "Error: No token provided, please login to access this page!"

@NoTokenNoAccess
Scenario: Try to access polling page with no login
	When I type in the polling page url
	Then I should be on the login page
	And I should see an error message "Error: No token provided, please login to access this page!"

@NoTokenNoAccess
Scenario: Try to access register page with no login
	When I type in the register page url
	Then I should be on the login page
	And I should see an error message "Error: No token provided, please login to access this page!"

@NoTokenNoAccess
Scenario: Try to press homepage button with no login
	And I am on the homepage
	When I press the Sparta logo
	Then I should be on the login page
	And I should see an error message "Error: No token provided, please login to access this page!"