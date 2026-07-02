# Dates

## Iso8601

ISO 8601 date or date-time string.

```csharp
bool isValid = Iso8601Pattern.IsValid(value);
```

**Constant:** `RegexPatterns.Iso8601`
**Validator:** `Iso8601Pattern`


## Rfc3339

RFC 3339 date-time string.

```csharp
bool isValid = Rfc3339Pattern.IsValid(value);
```

**Constant:** `RegexPatterns.Rfc3339`
**Validator:** `Rfc3339Pattern`


## DateYmd

Date in yyyy-MM-dd format.

```csharp
bool isValid = DateYmdPattern.IsValid(value);
```

**Constant:** `RegexPatterns.DateYmd`
**Validator:** `DateYmdPattern`


## Time

24-hour time, HH:mm or HH:mm:ss.

```csharp
bool isValid = TimePattern.IsValid(value);
```

**Constant:** `RegexPatterns.Time`
**Validator:** `TimePattern`


## TimeZone

UTC offset timezone designator (e.g. +05:30, Z).

```csharp
bool isValid = TimeZonePattern.IsValid(value);
```

**Constant:** `RegexPatterns.TimeZone`
**Validator:** `TimeZonePattern`


