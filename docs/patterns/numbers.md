# Numbers

## Integer

Signed or unsigned integer.

```csharp
bool isValid = IntegerPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Integer`
**Validator:** `IntegerPattern`


## Decimal

Signed or unsigned decimal number.

```csharp
bool isValid = DecimalPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Decimal`
**Validator:** `DecimalPattern`


## Currency

Currency amount, optional symbol and thousands separators.

```csharp
bool isValid = CurrencyPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Currency`
**Validator:** `CurrencyPattern`


## ScientificNotation

Number in scientific notation (e.g. 1.23e+10).

```csharp
bool isValid = ScientificNotationPattern.IsValid(value);
```

**Constant:** `RegexPatterns.ScientificNotation`
**Validator:** `ScientificNotationPattern`


