using AutoMapper;

namespace Nameless.AdventureWorks.IdentityServer.Api.v1.Mappings {
    public sealed class AuthenticationOutput_AuthenticationResponse : Profile {
        #region Public Constructors

        public AuthenticationOutput_AuthenticationResponse() {
            CreateMap<AuthenticationResponse, AuthenticationOutput>();
        }

        #endregion
    }
}
