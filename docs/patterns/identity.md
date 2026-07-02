# Identity

## Email

Standard email address (local-part@domain), ASCII-only per RFC 5322 local-part rules.

```csharp
bool isValid = EmailPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Email`
**Validator:** `EmailPattern`

!!! warning "Limitation"
    Does not support internationalized email (RFC 6531). Local-part cannot start, end with, or contain consecutive dots.


## Username

3-20 characters, alphanumeric with underscores/hyphens, must start with a letter.

```csharp
bool isValid = UsernamePattern.IsValid(value);
```

**Constant:** `RegexPatterns.Username`
**Validator:** `UsernamePattern`


## PasswordStrong

8-128 characters with at least one uppercase, lowercase, digit, and special character.

```csharp
bool isValid = PasswordStrongPattern.IsValid(value);
```

**Constant:** `RegexPatterns.PasswordStrong`
**Validator:** `PasswordStrongPattern`

!!! warning "Limitation"
    Capped at 128 characters as a defensive bound, not a security requirement.


## DisplayName

1-50 characters: letters, spaces, apostrophes, hyphens.

```csharp
bool isValid = DisplayNamePattern.IsValid(value);
```

**Constant:** `RegexPatterns.DisplayName`
**Validator:** `DisplayNamePattern`


## FullName

Two or more space-separated name parts, each starting with a letter.

```csharp
bool isValid = FullNamePattern.IsValid(value);
```

**Constant:** `RegexPatterns.FullName`
**Validator:** `FullNamePattern`

!!! warning "Limitation"
    Does not enforce capitalization.


