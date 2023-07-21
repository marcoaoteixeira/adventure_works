using System.Collections;

namespace Nameless.AdventureWorks.ProducerConsumer {
    public abstract class ArgCollection : IEnumerable<Arg> {
        #region Private Read-Only Fields

        private readonly Dictionary<string, Arg> _dictionary = new();

        #endregion

        #region Public Methods

        public void Set(string name, object value) {
            var arg = new Arg(name, value);

            if (!_dictionary.ContainsKey(name)) {
                _dictionary.Add(name, arg);
            } else {
                _dictionary[name] = arg;
            }
        }

        public object? Get(string name)
            => _dictionary.TryGetValue(name, out var value) ? value.Value : null;

        #endregion

        #region IEnumerable<Arg> Members

        public IEnumerator<Arg> GetEnumerator()
            => _dictionary.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => _dictionary.Values.GetEnumerator();

        #endregion
    }
}
