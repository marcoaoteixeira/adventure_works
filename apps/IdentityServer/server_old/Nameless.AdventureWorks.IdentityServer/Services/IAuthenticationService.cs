namespace Nameless.AdventureWorks.IdentityServer.Services {
    public interface IAuthenticationService {
        #region Methods

        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, CancellationToken cancellationToken = default);

        #endregion
    }

    public record AuthenticationResponse {
        #region Public Properties

        public object? UserId { get; }
        public string? FirstName { get; }
        public string? LastName { get; }
        public string? UserName { get; }
        public string? Email { get; }
        public string? Jwt { get; }
        public string? Error { get; }
        public bool Successful => Error == null;

        #endregion

        #region Private Constructors

        private AuthenticationResponse(object? userId, string? firstName, string? lastName, string? userName, string? email, string? jwt, string? error) {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Jwt = jwt;
            Error = error;
        }

        #endregion

        #region Public Static Methods

        public static AuthenticationResponse Success(object userId, string firstName, string lastName, string userName, string email, string jwt) {
            return new(
                userId: Prevent.Against.Null(userId, nameof(userId)),
                firstName: Prevent.Against.NullOrWhiteSpace(firstName, nameof(firstName)),
                lastName: Prevent.Against.NullOrWhiteSpace(lastName, nameof(lastName)),
                userName: Prevent.Against.NullOrWhiteSpace(userName, nameof(userName)),
                email: Prevent.Against.NullOrWhiteSpace(email, nameof(email)),
                jwt: Prevent.Against.NullOrWhiteSpace(jwt, nameof(jwt)),
                error: null
            );
        }

        public static AuthenticationResponse Failure(string error) {
            return new AuthenticationResponse(
                userId: null,
                firstName: null,
                lastName: null,
                userName: null,
                email: null,
                jwt: null,
                error: Prevent.Against.NullOrWhiteSpace(error, nameof(error))
            );
        }

        #endregion
    }

    public record AuthenticationRequest {
        #region Public Properties

        public string Identifier { get; }
        public string Password { get; }
        public string IpAddress { get; }

        #endregion

        #region Public Constructors

        public AuthenticationRequest(string identifier, string password, string ipAddress) {
            Identifier = Prevent.Against.NullOrWhiteSpace(identifier, nameof(identifier));
            Password = Prevent.Against.NullOrWhiteSpace(password, nameof(password));
            IpAddress = Prevent.Against.NullOrWhiteSpace(ipAddress, nameof(ipAddress));
        }

        #endregion
    }
}
