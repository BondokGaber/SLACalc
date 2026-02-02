Project Overview
A  Web API application for calculating Service Level Agreement (SLA) deadlines based on configured business hours, weekends, and holidays.
The application follows Clean Architecture principles with proper separation of concerns.

🏗️ Architecture
Clean Architecture Layers

SlaCalculation.API/
├── Domain/          # Core business entities and interfaces
├── Application/     # Use cases and business logic
├── Infrastructure/  # External concerns (DB, Services)
└── API/            # Presentation layer (Controllers, Middleware)

Database Schema

i do with both Database First and Code First approaches.
i face some challenges with data seeding and spent a lot of time to solve the problem so i make a sql insertion  In order to gain some time.
(Database Schema and Data script attached Here)


** Core Components

1. Domain Layer
Entities:
BaseEntity: Abstract base class with Id, CreatedAt, ModifiedAt

BusinessHour: Day of week, start/end times, active status

BusinessClosure: Date, optional time range, description, recurring flag

SlaConfiguration: Priority level and corresponding SLA hours

Value Objects:
SlaCalculationResult: Immutable result of SLA calculation

Interfaces:
IBusinessHourCalculator: Core SLA calculation logic

IRepository<T>: Generic repository pattern

IBusinessHourRepository, IBusinessClosureRepository, ISlaConfigurationRepository

2. Application Layer
Features:
CalculateSlaCommand: CQRS command for SLA calculation

GetPrioritiesQuery: Query for available priority levels

Behaviors:
ValidationBehavior: FluentValidation integration

LoggingBehavior: Request/response logging

Validators:
CalculateSlaCommandValidator: Validates input parameters

3. Infrastructure Layer
Data:
ApplicationDbContext: Entity Framework DbContext

Configurations: Entity type configurations with seed data for BusinessHours and SlaConfigurations

Repositories: Entity Framework implementations of domain interfaces

Services:
BusinessHourCalculator: Implements the recommended algorithm:

Add SLA hours to capture time
Loop through hours, checking business operations
Skip non-business hours (weekends, holidays)
Adjust deadline accordingly
LocalFileStorageService: Handles file uploads to local storage


4. API Layer
Controllers:
SlaController:

POST /api/sla/calculate: Calculate SLA deadline

GET /api/sla/priorities: Get available priorities



Middleware:
ExceptionHandlingMiddleware: Global error handling

CORS Configuration: Allows Angular frontend



------SLA Calculation Algorithm--------


Implementation Steps:
Initial Calculation: Add SLA hours to capture time

Hour-by-Hour Adjustment:

Start from next hour after capture

Loop while remaining business hours > 0

If hour is business hour: decrement remaining hours

If hour is non-business: extend deadline by 1 hour

Business Hour Check:

Verify day is business day (Monday-Friday)

Check time is within configured working hours

Exclude holidays and special closures

Handle partial day closures


---- Security & Validation


Input Validation:
Priority: Required, must exist in configurations

CaptureDateTime: Required, cannot be future date

Files: Optional, size and type validation

Error Handling:
Global exception middleware

Consistent error response format

Detailed logging

 File Upload Feature
Configuration:
Files stored in Uploads/ directory

Unique filenames with GUID prefix

Support for images, PDFs, text files

10MB file size limit per file



 Dependencies
NuGet Packages:

Microsoft.EntityFrameworkCore.SqlServer: 6.0.0

MediatR: 12.0.1 (CQRS pattern)

FluentValidation: 11.5.0 (Input validation)

Swashbuckle.AspNetCore: 6.5.0 (API documentation)

MediatR.Extensions.Microsoft.DependencyInjection

FluentValidation.DependencyInjectionExtensions




%%%  Patterns Used: %%%
Repository Pattern: Data access abstraction

CQRS: Command/Query separation

Mediator Pattern: Decoupled communication

Dependency Injection: Loose coupling



Planned Improvements:


Reporting Dashboard: SLA compliance metrics

Rate Limiting: Prevent abuse


Redis Caching: Performance optimization
