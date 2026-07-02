using RegexForge.Validation;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/users", (CreateUserRequest request) =>
{
    var errors = new List<string>();

    if (!EmailPattern.IsValid(request.Email))
        errors.Add("Email is not a valid email address.");

    if (!UsernamePattern.IsValid(request.Username))
        errors.Add("Username must be 3-20 characters, start with a letter, and contain only letters, digits, underscores, or hyphens.");

    if (!PasswordStrongPattern.IsValid(request.Password))
        errors.Add("Password must be at least 8 characters and include uppercase, lowercase, a digit, and a special character.");

    if (errors.Count > 0)
    {
        return Results.ValidationProblem(
            errors.ToDictionary(_ => "validation", e => new[] { e }),
            title: "One or more fields failed validation.");
    }

    return Results.Created($"/users/{request.Username}", new
    {
        request.Username,
        request.Email,
        Message = "User created successfully."
    });
});

app.MapGet("/", () => "RegexForge Minimal API Sample. POST to /users to try validation.");

app.Run();

internal sealed record CreateUserRequest(string Email, string Username, string Password);

public partial class Program { }
