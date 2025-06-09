# Alyio.Extensions

![Build Status](https://github.com/ousiax/Alyio.Extensions/actions/workflows/ci.yml/badge.svg?branch=main)

*Alyio.Extensions* is a .NET library that provides extension methods for converting between base data types. It supports .NET Standard 1.3 and 2.0, making it compatible with a wide range of .NET applications.

## Features

- **Object Extensions**: Methods for converting objects to various data types
- **String Extensions**: String manipulation and conversion utilities
- **DateTime Extensions**: DateTime formatting and conversion helpers
- **Byte Extensions**: Byte array conversion utilities

## Installation

You can install the package via NuGet:

```bash
dotnet add package Alyio.Extensions
```

Or using the NuGet Package Manager:

```
Install-Package Alyio.Extensions
```

## Usage

```csharp
using Alyio.Extensions;

// String to number conversion - returns null if conversion fails
string validNumber = "123";
int? convertedNumber = validNumber.ToInt32(); // Returns 123

string invalidNumber = "abc";
int? failedNumber = invalidNumber.ToInt32(); // Returns null because "abc" cannot be converted to int

// Object conversion - returns null if conversion fails
object validValue = "42";
int? validResult = validValue.ToInt32(); // Returns 42

object invalidValue = "not a number";
int? invalidResult = invalidValue.ToInt32(); // Returns null because "not a number" cannot be converted to int

// DateTime parsing - returns null if parsing fails
string validDate = "2024-03-20";
DateTime? validDateTime = validDate.ToDateTime(); // Returns DateTime object

string invalidDate = "not a date";
DateTime? invalidDateTime = invalidDate.ToDateTime(); // Returns null because "not a date" cannot be parsed

// Handling conversion results
string? input = "123";
int result = input.ToInt32() ?? -1; // Returns 123 if conversion succeeds, -1 if it fails
```

## Supported Frameworks

- .NET Standard 1.3
- .NET Standard 2.0

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Author

Jon X
