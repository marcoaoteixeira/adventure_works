﻿using System.Runtime.Serialization;

namespace Nameless.AdventureWorks.IdentityServer {

    [Serializable]
    public class AuthenticationException : Exception {
        #region Public Constructors

        public AuthenticationException() { }
        public AuthenticationException(string message) : base(message) { }
        public AuthenticationException(string message, Exception inner) : base(message, inner) { }

        #endregion

        #region Protected Constructors

        protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context) { } 

        #endregion
    }
}
