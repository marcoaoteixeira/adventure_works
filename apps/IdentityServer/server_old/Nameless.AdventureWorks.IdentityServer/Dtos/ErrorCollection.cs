using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.AdventureWorks {
    public sealed record Error(string Code, string Message);

    public sealed class ErrorCollection : IEnumerable<Error> {
        #region IEnumerable<Error> Members

        public IEnumerator<Error> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        } 

        #endregion
    }
}
