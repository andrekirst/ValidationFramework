# ValidationFramework

The Validation Frameowrk is a framework to validate a single or bulk input.

## Sonarqube-Status

[![Quality Gate](https://sonarcloud.io/api/badges/gate?key=andrekirst:ValidationFramework)](https://sonarcloud.io/dashboard/index/andrekirst:ValidationFramework) [![Lines](https://sonarcloud.io/api/badges/measure?key=andrekirst:ValidationFramework&metric=lines)](https://sonarcloud.io/dashboard/index/andrekirst:ValidationFramework) [![code smells](https://sonarcloud.io/api/badges/measure?key=andrekirst:ValidationFramework&metric=code_smells)](https://sonarcloud.io/dashboard/index/andrekirst:ValidationFramework) [![bugs](https://sonarcloud.io/api/badges/measure?key=andrekirst:ValidationFramework&metric=bugs)](https://sonarcloud.io/dashboard/index/andrekirst:ValidationFramework) [![vulnerabilities](https://sonarcloud.io/api/badges/measure?key=andrekirst:ValidationFramework&metric=vulnerabilities)](https://sonarcloud.io/dashboard/index/andrekirst:ValidationFramework) [![sqale_debt_ratio](https://sonarcloud.io/api/badges/measure?key=andrekirst:ValidationFramework&metric=sqale_debt_ratio)](https://sonarcloud.io/dashboard/index/andrekirst:ValidationFramework) [![reliability rating](https://sonarcloud.io/api/badges/measure?key=andrekirst:ValidationFramework&metric=reliability_rating)](https://sonarcloud.io/dashboard/index/andrekirst:ValidationFramework) [![security rating](https://sonarcloud.io/api/badges/measure?key=andrekirst:ValidationFramework&metric=security_rating)](https://sonarcloud.io/dashboard/index/andrekirst:ValidationFramework) [![coverage](https://sonarcloud.io/api/badges/measure?key=andrekirst:ValidationFramework&metric=coverage)](https://sonarcloud.io/dashboard/index/andrekirst:ValidationFramework)

## Examples

### Example 1 - Validator with simple inline validation with fluent

File: *Example.cs*

```csharp
// Create new Validator with type int
Validator<int> validator = new Validator<int>()
    // Add a new (inline) validation
    .Add(new Validation<int>(
        messageOnError: "Value is smaller than 3",
        messageOnSuccess: "Value is equal or greather than 3",
        name: "CHECK_VALUE_EQUAL_OR_GREATHER_THAN_3",
        originalValue: (i) => i,
        validationFunction: (i) => i >= 3));

int valuetoCheck = 2;

Console.WriteLine("Validate the value");
var responses = validator.Validate(value: valuetoCheck);

foreach (var response in responses)
{
    Console.WriteLine($"{response.Message}");
}

```

### Example 2 - Validator with simple inline validation

File: *Example.cs*

```csharp
// Create new Validator with type int
Validator<int> validator = new Validator<int>();
// Add a new validation
validator.AddValidation(
    new Validation<int>(
        messageOnError: "Value is smaller than 3",
        messageOnSuccess: "Value is equal or greather than 3",
        name: "CHECK_VALUE_EQUAL_OR_GREATHER_THAN_3",
        originalValue: (i) => i,
        validationFunction: (i) => i >= 3));

int valuetoCheck = 2;

Console.WriteLine("Validate the value");
var responses = validator.Validate(value: valuetoCheck);

foreach (var response in responses)
{
    Console.WriteLine($"{response.Message}");
}
```

### Example 3 - A validation class in a seperate class

File: *Example.cs*

```csharp
// Create new Validator with type int
Validator<int> validator = new Validator<int>()
    // Add a new validation
    .Add(new ValidationCheckIntValueEqualOrGreatherThan3());

int valuetoCheck = 2;

Console.WriteLine("Validate the value");
var responses = validator.Validate(value: valuetoCheck);

foreach (var response in responses)
{
    Console.WriteLine($"{response.Message}");
}
```

File: *ValidationCheckIntValueEqualOrGreatherThan3.cs*

```csharp
public class ValidationCheckIntValueEqualOrGreatherThan3 : AbstractValidation<int>
{
    public override string Name => "CHECK_VALUE_EQUAL_OR_GREATHER_THAN_3";

    public override Func<int, bool> ValidationFunction => (i) => i >= 3;

    public override string MessageOnError => "Value is smaller than 3";

    public override Func<int, object> OriginalValue => (i) => i;

    public override string MessageOnSuccess => throw new NotImplementedException();
}
```