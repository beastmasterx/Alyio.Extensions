# Alyio.Extensions

![Build Status](https://github.com/ousiax/Alyio.Extensions/actions/workflows/ci.yml/badge.svg?branch=main)

*Alyio.Extensions* is a .NET library that provides a comprehensive set of robust and high-performance extension methods for seamless type conversions across various base data types. It aims to simplify common conversion tasks, reduce boilerplate code, and enhance code readability and maintainability.

## Features

-   **Object Extensions**: Offers flexible methods to convert any object to various primitive types (`bool`, `int`, `long`, `double`, `decimal`, `DateTime`, `DateTimeOffset`, `DateOnly`, `enum`) with graceful handling of nulls and conversion failures.
    *   `ToBoolean()`: Converts an object to a boolean.
    *   `ToInt32()`, `ToInt64()`: Converts an object to a 32-bit or 64-bit integer.
    *   `ToDouble()`, `ToDecimal()`: Converts an object to a double or decimal.
    *   `ToDateTime()`, `ToDateTimeOffset()`, `ToDateOnly()`: Converts an object to `DateTime`, `DateTimeOffset`, or `DateOnly`.
    *   `ToEnum<T>()`: Converts an object to a specified enumeration type.
    *   `ToStringOrEmpty()`: Safely converts an object to its string representation, returning `string.Empty` for nulls.

-   **String Extensions**: Provides utilities for converting string representations to various data types, handling different formats and cultural conventions.
    *   `ToBoolean()`: Converts a string to a boolean, supporting "true"/"false" and numeric strings ("0" for false, non-zero for true).
    *   `ToInt32()`, `ToInt64()`, `ToDouble()`, `ToDecimal()`: Converts string representations of numbers to their respective types.
    *   `ToDateTime()`, `ToDateTimeOffset()`, `ToDateOnly()`: Parses strings into `DateTime`, `DateTimeOffset`, or `DateOnly` objects.
    *   `ToEnum<T>()`: Converts a string to a specified enumeration type.

-   **DateTime Extensions**: Simplifies common `DateTime` and `DateTimeOffset` operations, including Unix timestamp conversions and specific formatting.
    *   `ToDateInt32()`: Converts `DateTime` to an integer in `yyyyMMdd` format.
    *   `ToyyyyMMddHHmmss()`: Formats `DateTime` to "yyyy-MM-dd HH:mm:ss" string.
    *   `ToUnix()`: Converts `DateTime` or `DateTimeOffset` to a Unix timestamp (seconds since epoch).
    *   `ToDateTime()`: Converts Unix timestamp (long or double) or `DateTimeOffset` to `DateTime`.
    *   `ToDateTimeOffset()`: Converts `DateTime` or Unix timestamp (long) to `DateTimeOffset`.

-   **Byte Extensions**: Offers utilities for converting byte arrays to hexadecimal string representations.
    *   `ToHex()`: Converts a byte array to its hexadecimal string representation.

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
using System;
using System.Globalization;

// --- String Extensions ---

// String to number conversion - returns null if conversion fails
string validNumber = "123";
int? convertedNumber = validNumber.ToInt32(); // Returns 123

string invalidNumber = "abc";
int? failedNumber = invalidNumber.ToInt32(); // Returns null

// String to boolean conversion
"true".ToBoolean(); // Returns true
"false".ToBoolean(); // Returns false
"1".ToBoolean(); // Returns true (numeric string)
"0".ToBoolean(); // Returns false (numeric string)
"yes".ToBoolean(); // Returns false (not "true" or "false" or numeric)

// String to DateTimeOffset
string dateStringWithOffset = "2024-03-20T10:30:00+01:00";
DateTimeOffset? dto1 = dateStringWithOffset.ToDateTimeOffset(); // Returns DateTimeOffset with +01:00 offset

string dateStringWithoutOffset = "2024-03-20 10:30:00";
DateTimeOffset? dto2 = dateStringWithoutOffset.ToDateTimeOffset(); // Returns DateTimeOffset with local system's offset

// String to DateOnly (for .NET 6.0+)
#if NET6_0_OR_GREATER
string dateOnlyString = "2024-03-20";
DateOnly? dateOnly = dateOnlyString.ToDateOnly(); // Returns DateOnly(2024, 3, 20)
#endif

// --- Object Extensions ---

// Object to number conversion - returns null if conversion fails
object validValue = "42";
int? validResult = validValue.ToInt32(); // Returns 42

object invalidValue = "not a number";
int? invalidResult = invalidValue.ToInt32(); // Returns null

// Object to DateTimeOffset
object dateTimeObject = new DateTime(2024, 3, 20, 10, 30, 0, DateTimeKind.Utc);
DateTimeOffset? dtoFromObject = dateTimeObject.ToDateTimeOffset(); // Returns DateTimeOffset with UTC offset

object unixSecondsObject = 1710921000L; // Unix timestamp for 2024-03-20 10:30:00 UTC
DateTimeOffset? dtoFromUnix = unixSecondsObject.ToDateTimeOffset(); // Returns DateTimeOffset for 2024-03-20 10:30:00 UTC

// Object to Enum
public enum Status { None, Active, Inactive }
object statusString = "Active";
Status? enumStatus = statusString.ToEnum<Status>(); // Returns Status.Active

// --- DateTime Extensions ---

// DateTime to Unix timestamp
DateTime now = DateTime.UtcNow;
long unixTimestamp = now.ToUnix(); // Returns seconds since epoch

// Unix timestamp to DateTime
long someUnixTimestamp = 1678886400; // March 15, 2023 12:00:00 AM UTC
DateTime? fromUnix = someUnixTimestamp.ToDateTime(); // Returns DateTime for March 15, 2023 12:00:00 AM UTC

// DateTime to DateTimeOffset
DateTime localDt = new DateTime(2024, 3, 20, 10, 30, 0, DateTimeKind.Local);
DateTimeOffset localDto = localDt.ToDateTimeOffset(); // Uses local system's offset

// DateTimeOffset to Unix timestamp
DateTimeOffset currentDto = DateTimeOffset.UtcNow;
long dtoUnixTimestamp = currentDto.ToUnix();

// --- Byte Extensions ---

// Byte array to Hex string
byte[] data = { 0xDE, 0xAD, 0xBE, 0xEF };
string hexString = data.ToHex(); // Returns "DEADBEEF"

// Handling conversion results with null-coalescing operator
string? input = "123";
int result = input.ToInt32() ?? -1; // Returns 123 if conversion succeeds, -1 if it fails
```

## Supported Frameworks

-   .NET Standard 2.0
-   .NET 8.0
-   .NET 9.0

## Development

To build the project, navigate to the root directory of the repository and run:

```bash
dotnet build
```

To run the tests, execute:

```bash
dotnet test
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Author

Jon X