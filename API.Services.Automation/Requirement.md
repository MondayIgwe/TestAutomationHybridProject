


	API.Services.Automation/
├── Core/
│   ├── ApiClient.cs              # Base REST client setup
│   └── BaseTest.cs               # Common test setup/teardown
│
├── Services/                      # ← Service Object Layer (like PageObjects)
│   ├── AuthenticationService.cs  # Handles /api-clients endpoint
│   ├── BooksService.cs           # Handles /books endpoints
│   ├── OrdersService.cs          # Handles /orders endpoints
│   └── StatusService.cs          # Handles /status endpoint
│
├── Models/                        # Data Transfer Objects (DTOs)
│   ├── TokenResponse.cs
│   ├── OrderRequest.cs
│   ├── OrderResponse.cs
│   └── ErrorResponse.cs
│
├── StepDefinitions/               # BDD Gherkin step implementations
│   └── CreateBookOrderStepDefinitions.cs
│
├── Features/                      # Gherkin feature files
│   └── CreateOrder.feature
│
├── Hooks/                         # Test lifecycle hooks
│   └── TestHooks.cs
│
└── Utils/                         # Helper utilities
    └── ReusableValues.cs
