# Programming

## Guid

GUID/UUID, 8-4-4-4-12 hex format, with or without braces.

```csharp
bool isValid = GuidPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Guid`
**Validator:** `GuidPattern`


## Uuid

Alias of GuidPattern — identical pattern, provided for discoverability.

```csharp
bool isValid = UuidPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Uuid`
**Validator:** `UuidPattern`

!!! warning "Limitation"
    Functionally identical to GuidPattern.


## SemanticVersion

SemVer 2.0.0 compliant version string.

```csharp
bool isValid = SemanticVersionPattern.IsValid(value);
```

**Constant:** `RegexPatterns.SemanticVersion`
**Validator:** `SemanticVersionPattern`


## HexColor

Hex color code, 3/4/6/8 digits, with or without #.

```csharp
bool isValid = HexColorPattern.IsValid(value);
```

**Constant:** `RegexPatterns.HexColor`
**Validator:** `HexColorPattern`


## Base64

Base64-encoded string, standard alphabet.

```csharp
bool isValid = Base64Pattern.IsValid(value);
```

**Constant:** `RegexPatterns.Base64`
**Validator:** `Base64Pattern`


## CssColor

CSS color: hex, rgb(), or rgba().

```csharp
bool isValid = CssColorPattern.IsValid(value);
```

**Constant:** `RegexPatterns.CssColor`
**Validator:** `CssColorPattern`


## HtmlTag

A single HTML tag (opening, closing, or self-closing).

```csharp
bool isValid = HtmlTagPattern.IsValid(value);
```

**Constant:** `RegexPatterns.HtmlTag`
**Validator:** `HtmlTagPattern`

!!! warning "Limitation"
    Shallow structural match only — not a substitute for a real HTML parser. Will not correctly handle nested or malformed markup.


