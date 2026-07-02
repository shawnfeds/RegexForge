using RegexForge.Validation;

Console.WriteLine("RegexForge Console Sample");
Console.WriteLine("==========================\n");

// IsValid — the most common entry point
Console.WriteLine("-- IsValid --");
PrintValidation("Email", "john.doe@example.com", EmailPattern.IsValid("john.doe@example.com"));
PrintValidation("Email", "not-an-email", EmailPattern.IsValid("not-an-email"));
PrintValidation("Guid", "550e8400-e29b-41d4-a716-446655440000", GuidPattern.IsValid("550e8400-e29b-41d4-a716-446655440000"));
PrintValidation("Ipv4", "192.168.1.1", Ipv4Pattern.IsValid("192.168.1.1"));
PrintValidation("Ipv4", "999.999.999.999", Ipv4Pattern.IsValid("999.999.999.999"));

// Match — extract the matched substring
Console.WriteLine("\n-- Match --");
var emailMatch = EmailPattern.Match("Contact us at support@regexforge.dev for help.");
Console.WriteLine(emailMatch.Success
    ? $"Found email: {emailMatch.Value}"
    : "No email found.");

// Matches — find every occurrence
Console.WriteLine("\n-- Matches --");
var allEmails = EmailPattern.Matches("Reach alice@example.com or bob@example.com anytime.");
Console.WriteLine($"Found {allEmails.Count} email(s):");
foreach (System.Text.RegularExpressions.Match m in allEmails)
{
    Console.WriteLine($"  - {m.Value}");
}

// Replace — redact sensitive data
Console.WriteLine("\n-- Replace --");
var redacted = EmailPattern.Replace("My email is jane@example.com, please use it.", "[REDACTED]");
Console.WriteLine(redacted);

// TryMatch — non-throwing lookup pattern
Console.WriteLine("\n-- TryMatch --");
if (UrlPattern.TryMatch("Visit https://example.com for more info.", out var urlMatch))
{
    Console.WriteLine($"Found URL: {urlMatch!.Value}");
}

// Count
Console.WriteLine("\n-- Count --");
Console.WriteLine($"Hex colors found: {HexColorPattern.Count("Palette: #FF5733, #33FF57, #3357FF")}");

static void PrintValidation(string patternName, string input, bool isValid)
{
    Console.WriteLine($"{patternName,-8} '{input}' -> {(isValid ? "VALID" : "INVALID")}");
}
