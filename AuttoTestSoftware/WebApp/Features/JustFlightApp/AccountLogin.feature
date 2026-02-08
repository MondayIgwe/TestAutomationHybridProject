Feature: Account Login
  Short description: verify account logon behavior for the JustFlight app

  @smoke @login
  Scenario: Successful login with valid credentials
    Given I am on the account login page
    When I enter a valid email "autotestsoftware@gmail.com" and password "P@ssw0rd"
    And I submit the login form
    Then I should be logged in
    And I should see my account dashboard

  @negative @login
  Scenario Outline: Login fails with invalid or missing credentials
    Given I am on the account login page
    When I enter email "<email>" and password "<password>"
    And I submit the login form
    Then I should see an error message "<error>"

    Examples:
      | email             | password   | error                     |
      | user@example.com  | wrongpass  | Invalid email or password |
      | wrong@example.com | P@ssw0rd   | Invalid email or password |
      |                   | P@ssw0rd   | Email is required         |
      | user@example.com  |            | Password is required      |
