namespace ContactManager.Contracts.Authentication;

public record AuthenticationResponse
(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
);
