# Mobile.App.Automation - Requirements & Setup Guide

## Project Overview
**Mobile.App.Automation** is a **BDD (Behavior-Driven Development)** mobile test automation framework built with **Appium WebDriver** and **Reqnroll** for testing native, hybrid, and mobile web applications on **Android** and **iOS** platforms. The project follows the **Page Object Model (POM)** pattern and uses **NUnit** as the test runner.

---

## System Requirements

### Runtime
- **.NET 8.0** or later
- **Windows, macOS, or Linux** with .NET 8 SDK installed

### Mobile Testing Tools
- **Appium Server** (v2.0 or later) installed and running
- **Node.js** (v16 or later) for Appium installation
- **Java JDK** (v11 or later) for Android testing
- **Android Studio** with Android SDK (for Android testing)
- **Xcode** (v14 or later, macOS only for iOS testing)

### Mobile Devices & Emulators
- **Android**: Physical device or Android Emulator (AVD)
- **iOS**: Physical device (requires Apple Developer account) or iOS Simulator (macOS only)

### Tools & IDEs
- **Visual Studio 2022** (Community, Professional, or Enterprise) or **Visual Studio Code**
- **.NET 8 SDK** installed on your machine
- **Appium Inspector** (optional but recommended for element inspection)

---

## Project Dependencies

### NuGet Packages

| Package | Version | Purpose |
|---------|---------|---------|
| **Appium.WebDriver** | 8.1.0 | Appium client library for mobile automation (Android & iOS) |
| **Microsoft.NET.Test.Sdk** | 17.14.1 | Test execution framework and testing infrastructure |
| **Reqnroll.NUnit** | 3.2.0 | BDD framework integration with NUnit for Gherkin feature files |
| **NUnit** | 4.4.0 | Unit testing framework and assertions |
| **NUnit3TestAdapter** | 5.1.0 | Test adapter for running NUnit tests in Visual Studio |

---

## Appium Server Setup

### 1. Install Node.js
Download and install from: https://nodejs.org/

```bash
# Verify installation
node --version
npm --version
```

### 2. Install Appium Server (v2.0)
```bash
# Install Appium globally
npm install -g appium

# Verify installation
appium --version

# Install Appium drivers
appium driver install uiautomator2    # For Android
appium driver install xcuitest         # For iOS (macOS only)
```

### 3. Start Appium Server
```bash
# Start server on default port (4723)
appium

# Or specify custom host/port
appium --address 127.0.0.1 --port 4723
```

---

## Android Setup

### 1. Install Android Studio
Download from: https://developer.android.com/studio

### 2. Install Android SDK
- Open Android Studio ? SDK Manager
- Install latest Android SDK Platform (e.g., Android 14)
- Install Android SDK Build-Tools
- Install Android Emulator
- Install Intel x86 Emulator Accelerator (HAXM) for faster emulation

### 3. Set Environment Variables
**Windows:**
```powershell
# Add to System Environment Variables
ANDROID_HOME = C:\Users\<YourUsername>\AppData\Local\Android\Sdk
Path += %ANDROID_HOME%\platform-tools
Path += %ANDROID_HOME%\tools
Path += %ANDROID_HOME%\emulator
```

**macOS/Linux:**
```bash
# Add to ~/.bashrc or ~/.zshrc
export ANDROID_HOME=$HOME/Library/Android/sdk
export PATH=$PATH:$ANDROID_HOME/platform-tools
export PATH=$PATH:$ANDROID_HOME/tools
export PATH=$PATH:$ANDROID_HOME/emulator
```

### 4. Verify Installation
```bash
# Check ADB (Android Debug Bridge)
adb version

# List connected devices
adb devices
```

### 5. Create Android Emulator (AVD)
```bash
# List available system images
sdkmanager --list | grep system-images

# Create emulator
avdmanager create avd -n Pixel_6_API_34 -k "system-images;android-34;google_apis;x86_64" -d pixel_6

# Start emulator
emulator -avd Pixel_6_API_34
```

---

## iOS Setup (macOS Only)

### 1. Install Xcode
Download from Mac App Store or: https://developer.apple.com/xcode/

### 2. Install Xcode Command Line Tools
```bash
xcode-select --install
```

### 3. Install Carthage (dependency manager)
```bash
brew install carthage
```

### 4. Install ios-deploy (for real devices)
```bash
npm install -g ios-deploy
```

### 5. Configure iOS Simulator
```bash
# List available simulators
xcrun simctl list devices

# Boot a simulator
xcrun simctl boot "iPhone 15 Pro"

# Open Simulator app
open -a Simulator
```

### 6. WebDriverAgent Setup (for iOS automation)
```bash
# Navigate to WebDriverAgent
cd /usr/local/lib/node_modules/appium/node_modules/appium-xcuitest-driver/node_modules/appium-webdriveragent

# Install dependencies
carthage update

# Open project in Xcode
open WebDriverAgent.xcodeproj
```

**In Xcode:**
1. Select WebDriverAgentLib target ? Signing & Capabilities
2. Select your Team (Apple Developer account required)
3. Build the project (Cmd + B)

---

## Project Structure

```
Mobile.App.Automation/
??? Drivers/                          # WebDriver/Appium driver management
?   ??? AppiumDriverFactory.cs       # Factory for creating Android/iOS drivers
?
??? Support/                          # Helper utilities
?   ??? AppiumActions.cs             # Common Appium actions (tap, swipe, scroll)
?   ??? DeviceCapabilities.cs        # Desired capabilities for devices
?   ??? ConfigHelper.cs              # Configuration and settings
?
??? PageObjects/                      # Page Object Model for mobile screens
?   ??? Android/
?   ?   ??? CalculatorPage.cs       # Android Calculator screen
?   ??? iOS/
?       ??? CalculatorPage.cs       # iOS Calculator screen
?
??? StepDefinitions/                  # BDD step definitions
?   ??? CalculatorStepDefinitions.cs # Step implementations
?
??? Features/                         # Gherkin feature files
?   ??? Calculator.feature           # BDD test scenarios
?
??? Hooks/                            # BDD lifecycle hooks
?   ??? TestHooks.cs                 # Before/After scenario hooks
?
??? Mobile.App.Automation.csproj     # Project configuration
```

---

## Appium Desired Capabilities

### Android Capabilities Example
```csharp
var options = new AppiumOptions();
options.AddAdditionalAppiumOption("platformName", "Android");
options.AddAdditionalAppiumOption("platformVersion", "14");
options.AddAdditionalAppiumOption("deviceName", "Pixel_6_API_34");
options.AddAdditionalAppiumOption("automationName", "UiAutomator2");
options.AddAdditionalAppiumOption("app", "/path/to/your/app.apk");
options.AddAdditionalAppiumOption("appPackage", "com.example.app");
options.AddAdditionalAppiumOption("appActivity", "com.example.app.MainActivity");
options.AddAdditionalAppiumOption("noReset", true);
```

### iOS Capabilities Example
```csharp
var options = new AppiumOptions();
options.AddAdditionalAppiumOption("platformName", "iOS");
options.AddAdditionalAppiumOption("platformVersion", "17.0");
options.AddAdditionalAppiumOption("deviceName", "iPhone 15 Pro");
options.AddAdditionalAppiumOption("automationName", "XCUITest");
options.AddAdditionalAppiumOption("app", "/path/to/your/app.app");
options.AddAdditionalAppiumOption("bundleId", "com.example.app");
options.AddAdditionalAppiumOption("noReset", true);
```

---

## Key Features

### BDD Framework (Reqnroll)
- Write tests in **Gherkin** syntax (.feature files)
- **Given-When-Then** scenario structure for readable test cases
- **Scenario Outlines** with examples for parameterized tests

### Page Object Model (POM)
- Separate page classes for Android and iOS platforms
- Centralized element locators
- Reusable and maintainable test code

### Appium WebDriver
- Cross-platform support (Android & iOS)
- Native, hybrid, and mobile web app testing
- Gestures support (tap, swipe, scroll, pinch)

### NUnit Integration
- Standard assertions with NUnit 4.4
- Test execution and reporting
- Parallel test execution support

---

## Environment Variables

| Variable | Values | Default | Purpose |
|----------|--------|---------|---------|
| `PLATFORM` | `Android`, `iOS` | `Android` | Target mobile platform |
| `DEVICE_NAME` | Device/Emulator name | `Pixel_6_API_34` | Target device identifier |
| `PLATFORM_VERSION` | OS version | `14` | Mobile OS version |
| `APPIUM_HOST` | IP address | `127.0.0.1` | Appium server host |
| `APPIUM_PORT` | Port number | `4723` | Appium server port |
| `APP_PATH` | File path | - | Path to APK/APP file |

### Example Usage
```bash
# Run tests on Android emulator
$env:PLATFORM = "Android"
$env:DEVICE_NAME = "Pixel_6_API_34"
dotnet test

# Run tests on iOS simulator
$env:PLATFORM = "iOS"
$env:DEVICE_NAME = "iPhone 15 Pro"
dotnet test
```

---

## Setup Instructions

### 1. Prerequisites
```bash
# Verify .NET 8 installation
dotnet --version

# Verify Appium installation
appium --version

# Verify Node.js
node --version

# Verify Android tools (for Android)
adb version
avdmanager list avd

# Verify iOS tools (for iOS on macOS)
xcodebuild -version
xcrun simctl list devices
```

### 2. Clone & Restore
```bash
git clone https://github.com/MondayIgwe/TestAutomationHybridProject
cd TestAutomationHybrid/Mobile.App.Automation
dotnet restore
```

### 3. Start Appium Server
```bash
# In a separate terminal
appium
```

### 4. Start Mobile Device/Emulator
**Android:**
```bash
emulator -avd Pixel_6_API_34
# Or connect physical device via USB and enable USB debugging
```

**iOS (macOS):**
```bash
xcrun simctl boot "iPhone 15 Pro"
open -a Simulator
```

### 5. Build Project
```bash
dotnet build
```

### 6. Run Tests
```bash
# Run all tests
dotnet test

# Run with verbose output
dotnet test --verbosity normal
```

---

## Common Commands

```bash
# Build project
dotnet build

# Run all mobile tests
dotnet test

# Run specific test by filter
dotnet test --filter "FullyQualifiedName~Calculator"

# Generate test report
dotnet test --logger "trx;LogFileName=mobile-test-results.trx"

# Clean build artifacts
dotnet clean

# Appium server commands
appium                              # Start server
appium driver list                  # List installed drivers
appium driver install uiautomator2  # Install Android driver
appium driver install xcuitest      # Install iOS driver

# Android commands
adb devices                         # List connected devices
adb shell                           # Open device shell
adb install app.apk                 # Install APK
adb uninstall com.package.name      # Uninstall app
adb logcat                          # View device logs

# iOS commands
xcrun simctl list devices           # List simulators
xcrun simctl boot <UDID>            # Boot simulator
xcrun simctl shutdown <UDID>        # Shutdown simulator
```

---

## Element Locator Strategies

### Android Locators
```csharp
// Resource ID
driver.FindElement(By.Id("com.example:id/button"));

// Accessibility ID
driver.FindElement(MobileBy.AccessibilityId("Login Button"));

// XPath
driver.FindElement(By.XPath("//android.widget.Button[@text='Login']"));

// UIAutomator (Android-specific)
driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Login\")"));

// Class name
driver.FindElement(By.ClassName("android.widget.Button"));
```

### iOS Locators
```csharp
// Accessibility ID
driver.FindElement(MobileBy.AccessibilityId("LoginButton"));

// XPath
driver.FindElement(By.XPath("//XCUIElementTypeButton[@name='Login']"));

// iOS Predicate String (iOS-specific)
driver.FindElement(MobileBy.IosNSPredicate("type == 'XCUIElementTypeButton' AND name == 'Login'"));

// iOS Class Chain (iOS-specific)
driver.FindElement(MobileBy.IosClassChain("**/XCUIElementTypeButton[`name == 'Login'`]"));

// Name
driver.FindElement(By.Name("Login"));
```

---

## Mobile Gestures

```csharp
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;

// Tap
TouchAction tap = new TouchAction(driver);
tap.Tap(element).Perform();

// Long press
TouchAction longPress = new TouchAction(driver);
longPress.LongPress(element).Perform();

// Swipe
TouchAction swipe = new TouchAction(driver);
swipe.Press(x1, y1).MoveTo(x2, y2).Release().Perform();

// Scroll (Android)
driver.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"Target Text\"))");

// Scroll (iOS)
Dictionary<string, object> scrollObject = new Dictionary<string, object>();
scrollObject.Add("direction", "down");
driver.ExecuteScript("mobile: scroll", scrollObject);
```

---

## Troubleshooting

### Appium Server Issues
- **Issue**: `EADDRINUSE: address already in use`
- **Solution**: Kill existing Appium process or use different port
```bash
# Find process using port 4723
lsof -i :4723  # macOS/Linux
netstat -ano | findstr :4723  # Windows

# Start on different port
appium --port 4724
```

### Android Device Not Found
- **Issue**: `adb devices` shows no devices
- **Solution**: 
  - Enable USB debugging on device
  - Install USB drivers (Windows)
  - Restart ADB: `adb kill-server && adb start-server`

### iOS Simulator Not Starting
- **Issue**: Simulator fails to boot
- **Solution**:
```bash
# Reset simulator
xcrun simctl shutdown all
xcrun simctl erase all

# Restart CoreSimulator service
sudo killall -9 com.apple.CoreSimulator.CoreSimulatorService
```

### WebDriverAgent Signing Error (iOS)
- **Issue**: WebDriverAgent build fails
- **Solution**: Update signing in Xcode with valid Apple Developer Team ID

### Element Not Found
- **Issue**: `NoSuchElementException`
- **Solution**:
  - Use Appium Inspector to verify locators
  - Add explicit waits
  - Check if element is in a different context (native vs webview)

---

## Appium Inspector Setup

### Installation
```bash
# Download from GitHub releases
# https://github.com/appium/appium-inspector/releases

# Or install via npm
npm install -g appium-inspector
```

### Usage
1. Start Appium server
2. Start Appium Inspector
3. Configure desired capabilities
4. Click "Start Session"
5. Inspect elements and generate locators

---

## CI/CD Integration

### GitHub Actions Example
```yaml
name: Mobile Tests

on: [push, pull_request]

jobs:
  android-tests:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: 8.0.x
      
      - name: Setup Java
        uses: actions/setup-java@v3
        with:
          distribution: 'temurin'
          java-version: '11'
      
      - name: Setup Android SDK
        uses: android-actions/setup-android@v2
      
      - name: Install Appium
        run: npm install -g appium && appium driver install uiautomator2
      
      - name: Start Appium
        run: appium &
      
      - name: Create AVD and start emulator
        run: |
          echo "y" | sdkmanager "system-images;android-34;google_apis;x86_64"
          avdmanager create avd -n test -k "system-images;android-34;google_apis;x86_64" --force
          emulator -avd test -no-snapshot-save -no-window -gpu swiftshader_indirect -noaudio -no-boot-anim &
      
      - name: Wait for emulator
        run: adb wait-for-device shell 'while [[ -z $(getprop sys.boot_completed) ]]; do sleep 1; done'
      
      - name: Run tests
        run: dotnet test Mobile.App.Automation
```

---

## Technology Stack Summary

| Layer | Technology | Version |
|-------|-----------|---------|
| **Runtime** | .NET | 8.0 |
| **Test Framework** | NUnit | 4.4.0 |
| **BDD Framework** | Reqnroll | 3.2.0 |
| **Mobile Automation** | Appium WebDriver | 8.1.0 |
| **Android Driver** | UiAutomator2 | Latest |
| **iOS Driver** | XCUITest | Latest |
| **Language** | C# | 12.0 |

---

## Best Practices

### 1. Use Page Object Model
- Separate page classes for each screen
- Encapsulate element locators in page classes
- Reuse page methods across tests

### 2. Platform-Specific Code
```csharp
if (driver.PlatformName == "Android")
{
    // Android-specific logic
}
else if (driver.PlatformName == "iOS")
{
    // iOS-specific logic
}
```

### 3. Explicit Waits
```csharp
WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
wait.Until(driver => driver.FindElement(By.Id("element")));
```

### 4. Handle App States
```csharp
// Background app
driver.BackgroundApp(TimeSpan.FromSeconds(5));

// Reset app
driver.ResetApp();

// Close app
driver.CloseApp();

// Launch app
driver.LaunchApp();
```

### 5. Context Switching (Hybrid Apps)
```csharp
// Switch to webview
driver.Context = driver.Contexts.Last();

// Switch back to native
driver.Context = "NATIVE_APP";
```

---

## Future Enhancements

- [ ] Cross-platform page objects (shared interface)
- [ ] Screenshot/video recording on failure
- [ ] Performance metrics collection
- [ ] Accessibility testing integration
- [ ] Cloud device testing (BrowserStack, Sauce Labs, AWS Device Farm)
- [ ] Parallel execution across devices
- [ ] Biometric authentication testing
- [ ] Deep linking testing
- [ ] Push notification testing

---

## Resources & Documentation

- **Appium Official Docs**: https://appium.io/docs/
- **Appium .NET Client**: https://github.com/appium/dotnet-client
- **Reqnroll Docs**: https://reqnroll.net/
- **NUnit Docs**: https://nunit.org/
- **Android Developer Docs**: https://developer.android.com/
- **iOS Developer Docs**: https://developer.apple.com/documentation/

---

## Contact & Support

For issues, questions, or contributions, please refer to the project's GitHub repository:
https://github.com/MondayIgwe/TestAutomationHybridProject

---

**Last Updated**: 2024  
**Framework Version**: Appium 8.1.0 + Reqnroll 3.2.0 + NUnit 4.4.0  
**Target Framework**: .NET 8.0  
**Supported Platforms**: Android (UiAutomator2) & iOS (XCUITest)
