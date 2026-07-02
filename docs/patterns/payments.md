# Payments

## Iban

Structural shape of an IBAN.

```csharp
bool isValid = IbanPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Iban`
**Validator:** `IbanPattern`

!!! warning "Limitation"
    Validates structure only — does not verify the MOD-97 checksum or country-specific length rules.


## Swift

SWIFT/BIC bank identifier code, 8 or 11 characters.

```csharp
bool isValid = SwiftPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Swift`
**Validator:** `SwiftPattern`


