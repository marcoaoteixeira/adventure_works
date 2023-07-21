using AutoMapper;

namespace Nameless.AdventureWorks.IdentityServer.Api.v1.Mappings {
    public sealed class User_CreateUserOutput : Profile {
        #region Public Constructors

        public User_CreateUserOutput()
        {
            CreateMap<User, CreateUserOutput>()
                .ForMember(_ => _.UserId, opts => opts.MapFrom(_ => _.Id.ToString()));
        }

        #endregion
    }
}
