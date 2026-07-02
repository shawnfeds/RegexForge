# Security

## Jwt

Structural shape of a JWT (three base64url segments).

```csharp
bool isValid = JwtPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Jwt`
**Validator:** `JwtPattern`

!!! warning "Limitation"
    Validates structure only, not signature or claims.


## ApiKey

General structural shape of an API key, 20-128 characters.

```csharp
bool isValid = ApiKeyPattern.IsValid(value);
```

**Constant:** `RegexPatterns.ApiKey`
**Validator:** `ApiKeyPattern`

!!! warning "Limitation"
    API key formats vary widely by provider — this is a permissive structural check, not validity against any specific issuer.


## Sha256

SHA-256 hash, 64 hex characters.

```csharp
bool isValid = Sha256Pattern.IsValid(value);
```

**Constant:** `RegexPatterns.Sha256`
**Validator:** `Sha256Pattern`


## Md5

MD5 hash, 32 hex characters.

```csharp
bool isValid = Md5Pattern.IsValid(value);
```

**Constant:** `RegexPatterns.Md5`
**Validator:** `Md5Pattern`


