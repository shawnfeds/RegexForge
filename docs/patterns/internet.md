# Internet

## Url

Absolute HTTP/HTTPS URL.

```csharp
bool isValid = UrlPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Url`
**Validator:** `UrlPattern`

!!! warning "Limitation"
    Does not match ftp:// or other schemes by design.


## Domain

Domain name (e.g. example.com).

```csharp
bool isValid = DomainPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Domain`
**Validator:** `DomainPattern`


## Host

RFC 1123 hostname, max 253 characters.

```csharp
bool isValid = HostPattern.IsValid(value);
```

**Constant:** `RegexPatterns.Host`
**Validator:** `HostPattern`


## Ipv4

IPv4 address in dotted-quad notation.

```csharp
bool isValid = Ipv4Pattern.IsValid(value);
```

**Constant:** `RegexPatterns.Ipv4`
**Validator:** `Ipv4Pattern`


## Ipv6

IPv6 address, full or compressed form.

```csharp
bool isValid = Ipv6Pattern.IsValid(value);
```

**Constant:** `RegexPatterns.Ipv6`
**Validator:** `Ipv6Pattern`


## MacAddress

MAC address, colon- or hyphen-separated.

```csharp
bool isValid = MacAddressPattern.IsValid(value);
```

**Constant:** `RegexPatterns.MacAddress`
**Validator:** `MacAddressPattern`


