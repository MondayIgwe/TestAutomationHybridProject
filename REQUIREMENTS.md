# AuttoTestSoftware - Requirements & Dependencies

## Project Overview
AuttoTestSoftware is a **BDD (Behavior-Driven Development)** test automation framework built with **Selenium WebDriver** and **Reqnroll** for testing web applications. The project follows the **Page Object Model (POM)** pattern and uses **NUnit** as the test runner.

---

## System Requirements

### Runtime
- **.NET 9.0** or later
- **Windows, macOS, or Linux** with .NET 9 SDK installed

### Browser
- **Google Chrome** (latest version)
- ChromeDriver installed and compatible with your Chrome version

### Tools & IDEs
- **Visual Studio 2022** (Community, Professional, or Enterprise) or **Visual Studio Code**
- **.NET 9 SDK** installed on your machine

---

## Project Dependencies

### NuGet Packages

| Package | Version | Purpose |
|---------|---------|---------|
| **Microsoft.NET.Test.Sdk** | 17.14.1 | Test execution framework and testing infrastructure |
| **Reqnroll.NUnit** | 3.2.0 | BDD framework integration with NUnit for Gherkin feature files |
| **NUnit** | 4.4.0 | Unit testing framework and assertions |
| **NUnit3TestAdapter** | 5.1.0 | Test adapter for running NUnit tests in Visual Studio |
| **Selenium.WebDriver** | 4.11.0 | Browser automation library for web application testing |
| **Selenium.WebDriver.ChromeDriver** | 144.0.7559.13300 | ChromeDriver for Chrome browser automation |
| **Shouldly** | 4.3.0 | BDD-style assertion library for fluent assertions |

---

## Project Structure

```
AuttoTestSoftware/
??? WebApp/
?   ??? Core/
?   ?   ??? BaseTest.cs                 # Base class for test setup/teardown
?   ?
?   ??? Support/
?   ?   ??? BrowserDriver.cs            # WebDriver management (start/quit)
?   ?   ??? SeleniumActions.cs          # Common Selenium helper methods
?   ?   ??? ISelenuimActions.cs         # Interface for Selenium actions
?   ?   ??? Commons.cs                  # Common utility methods
?   ?   ??? Locators.cs                 # Centralized element locators
?   ?
?   ??? PageObjects/
?   ?   ??? JustFlight/
?   ?       ??? AccountLoginPage.cs     # Page object for login functionality
?   ?
?   ??? StepDefinitions/
?   ?   ??? JustFlight/
?   ?       ??? AccountLoginStepDefinitions.cs  # BDD step definitions for login
?   ?
?   ??? Features/
?   ?   ??? JustFlightApp/
?   ?       ??? AccountLogin.feature    # Gherkin feature file for login tests
?   ?
?   ??? Hooks/
?       ??? TestHook.cs                 # BDD hooks (Before/AfterScenario)
?
??? AuttoTestSoftware.csproj            # Project file with NuGet dependencies
??? ImplicitUsings.cs                   # Global using statements
```

---

## Key Features

### BDD Framework (Reqnroll)
- Write tests in **Gherkin** syntax (.feature files)
- **Given-When-Then** scenario structure for readable test cases
- **Scenario Outlines** with examples for parameterized tests

### Page Object Model (POM)
- Encapsulate page interactions in dedicated page classes
- Centralized locators in `Locators.cs`
- Reusable and maintainable test code

### Selenium WebDriver
- Cross-browser support (currently configured for Chrome)
- Headless mode configurable via `HEADLESS_MODE` environment variable
  - `new` (default): Modern headless mode
  - `old`: Legacy headless mode
  - `off`: Visible browser window

### NUnit Integration
- Standard assertions with NUnit 4.4
- Test execution and reporting
- Fluent assertions with Shouldly library

### Test Hooks
- Automatic browser initialization before each scenario
- Automatic browser cleanup after each scenario
- Screenshot capture on test failure

---

## Environment Variables

| Variable | Values | Default | Purpose |
|----------|--------|---------|---------|
| `HEADLESS_MODE` | `new`, `old`, `off`, `false`, `no` | `new` | Controls Chrome headless behavior |

### Example Usage
```powershell
# Run tests in visible browser
$env:HEADLESS_MODE = "off"
dotnet test

# Run tests in legacy headless mode
$env:HEADLESS_MODE = "old"
dotnet test

# Run tests in modern headless mode (default)
dotnet test
```

---

## Setup Instructions

### 1. Prerequisites
```bash
# Verify .NET 9 installation
dotnet --version

# Verify Chrome is installed
chrome --version
```

### 2. Clone & Restore
```bash
git clone <repository-url>
cd AuttoTestSoftware
dotnet restore
```

### 3. Build Project
```bash
dotnet build
```

### 4. Run All Tests
```bash
dotnet test
```

### 5. Run Specific Tests by Tag
```bash
# Run only smoke tests
dotnet test --filter "Category=smoke"

# Run only login tests
dotnet test --filter "Category=login"
```

---

## Technology Stack Summary

| Layer | Technology | Version |
|-------|-----------|---------|
| **Runtime** | .NET | 9.0 |
| **Test Framework** | NUnit | 4.4.0 |
| **BDD Framework** | Reqnroll | 3.2.0 |
| **Browser Automation** | Selenium WebDriver | 4.11.0 |
| **Assertions** | Shouldly | 4.3.0 |
| **Browser** | Chrome | Latest |
| **Language** | C# | 13.0 |

---

## Configuration & Best Practices

### Browser Configuration (BrowserDriver.cs)
- Window size: 1280 x 1024px
- Browser sandbox disabled for CI/CD environments
- Dev SHM disabled for containerized environments

### Test Execution Flow
1. **[BeforeScenario Hook]** ? Start Chrome browser
2. **[Given Steps]** ? Set up test preconditions
3. **[When Steps]** ? Execute test actions
4. **[Then Steps]** ? Verify expected outcomes
5. **[AfterScenario Hook]** ? Close browser, capture screenshots on failure

### Locator Strategy
- Use **By.Id** for stable, unique elements
- Use **By.XPath** for complex element trees
- Use **By.CssSelector** as fallback
- All locators centralized in `Locators.cs`

---

## Common Commands

```bash
# Build project
dotnet build

# Run all tests
dotnet test

# Run with verbose output
dotnet test --verbosity normal

# Run specific feature file
dotnet test --filter "FullyQualifiedName~AccountLogin"

# Generate test report
dotnet test --logger "trx;LogFileName=test-results.trx"

# Clean build artifacts
dotnet clean
```

---

## Troubleshooting

### ChromeDriver Version Mismatch
- **Issue**: `SessionNotCreatedException`
- **Solution**: Update ChromeDriver version in `.csproj` to match your Chrome version
```xml
<PackageReference Include="Selenium.WebDriver.ChromeDriver" Version="<YourChromeVersion>" />
```

### Headless Mode Not Working
- **Issue**: Tests pass with visible browser but fail in headless
- **Solution**: Check `HEADLESS_MODE` environment variable and Chrome version compatibility

### Implicit Usings Issues
- **Issue**: `NUnit` or `Reqnroll` not found
- **Solution**: Ensure `ImplicitUsings.cs` has the correct global using statements

---

## Future Enhancements

- [ ] Multi-browser support (Firefox, Edge, Safari)
- [ ] Parallel test execution
- [ ] Screenshot/video recording on failure
- [ ] Allure reporting integration
- [ ] Docker containerization for CI/CD
- [ ] Performance benchmarking
- [ ] Accessibility testing (AXE)
- [ ] API testing integration

---

## Contact & Support

For issues, questions, or contributions, please refer to the project's repository and documentation.

---

**Last Updated**: 2024  
**Framework Version**: Selenium 4.11.0 + Reqnroll 3.2.0 + NUnit 4.4.0  
**Target Framework**: .NET 9.0
