using Nameless.AdventureWorks.IdentityServer.Services;
using Nameless.AdventureWorks.Models;

namespace Nameless.AdventureWorks.IdentityServer {
    public static class AuthenticationResponseExtension {
        #region Public Static Methods

        public static ClientErrorCollection ToClientError(this AuthenticationResponse self) {
            var result = new ClientErrorCollection();

            if (self.Error != null) {
                result.Push(nameof(AuthenticationResponse), self.Error);
            }

            return result;
        }

        #endregion
    }
}
