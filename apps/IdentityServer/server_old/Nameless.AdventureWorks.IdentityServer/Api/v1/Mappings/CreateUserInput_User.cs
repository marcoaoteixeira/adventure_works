using AutoMapper;

namespace Nameless.AdventureWorks.IdentityServer.Api.v1.Mappings {
    public sealed class CreateUserInput_User : Profile {
        #region Public Constructors

        public CreateUserInput_User() {
            CreateMap<CreateUserInput, User>()

                .ForMember(_ => _.FirstName, opts => opts.MapFrom(_ => _.FirstName))
                .ForMember(_ => _.LastName, opts => opts.MapFrom(_ => _.LastName))
                .ForMember(_ => _.UserName, opts => opts.MapFrom(_ => _.UserName))
                .ForMember(_ => _.Email, opts => opts.MapFrom(_ => _.Email))
                .ForMember(_ => _.PhoneNumber, opts => opts.MapFrom(_ => _.PhoneNumber))
                .ForMember(_ => _.AvatarUrl, opts => opts.MapFrom(_ => _.AvatarUrl));
        }

        #endregion
    }
}
