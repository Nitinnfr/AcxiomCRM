# Automated tests

A small xUnit test project is included for model-level validation of customer and opportunity fields. Run it from the project root with:

```powershell
dotnet test .\Tests\AcxiomCRM.Tests\AcxiomCRM.Tests.csproj
```

These tests do not replace integration tests for authentication, role authorization, database uniqueness, APIs, or workflow/business rules. Add those before production use.
