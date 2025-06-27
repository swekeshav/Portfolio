# ADR: Exception Handling Architecture for Razor Pages

## Status
Accepted

## Context
The application uses ASP.NET Core Razor Pages and requires a robust, extensible mechanism for handling exceptions in both API and UI (Razor) contexts. Different types of requests (AJAX/API vs. standard browser requests) and different exception scenarios (e.g., authentication failures, bad requests, unhandled errors) require tailored responses for a better user and developer experience.

## Decision
- **RazorExceptionHandler** is implemented to handle exceptions for standard Razor Page requests. It:
  - Checks if the request is an AJAX request and, if so, does not handle it (returns `false`).
  - Uses an injected `IExceptionPolicy` to determine if the user should be logged out (e.g., for authentication/authorization exceptions). If so, it signs out the user and redirects to the login page.
  - For other exceptions, it attempts to render a user-friendly error page using the Razor view engine. If the error view is missing, it writes a fallback message.
- **APIExceptionHandler** is implemented to handle exceptions for API requests. It:
  - Checks for specific exception types (e.g., `BadRequestException`) and returns a structured `ProblemDetails` response with appropriate status codes and error messages.
  - Logs errors for diagnostics and observability.
- Both handlers are designed to be easily testable and extensible, using dependency injection for their dependencies (e.g., view engine, temp data provider, exception policy, logger).

## Consequences
- Exception handling is separated by request type, improving maintainability and clarity.
- The use of `IExceptionPolicy` allows for flexible, centralized control over logout behavior.
- The architecture supports custom error pages and structured API error responses, improving user experience and API client integration.
- The design is testable, as shown by the use of dependency injection and the ability to mock dependencies in unit tests.

## Alternatives Considered
- Handling all exceptions in a single middleware or handler. This was rejected to allow for more granular, context-specific error handling and response formatting.

## Related Files
- `Portfolio.Web/ExceptionHandler/RazorExceptionHandler.cs`
- `Portfolio.Web/ExceptionHandler/APIExceptionHandler.cs`
- `Portfolio.Web/Extensions/RequestExtensions.cs`
- `Portfolio.Web/Models/ErrorViewModel.cs`
- `Portfolio.Web/IExceptionPolicy.cs`