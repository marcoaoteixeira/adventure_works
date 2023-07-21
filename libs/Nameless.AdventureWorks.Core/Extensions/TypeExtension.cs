namespace Nameless.AdventureWorks {
    public static class TypeExtension {
        #region Public Static Methods

        /// <summary>
        /// Checks if the current type (<paramref name="self"/>) is assignable from the <paramref name="type"/>.
        /// </summary>
        /// <param name="self">The current type.</param>
        /// <param name="type">The assignable from type.</param>
        /// <returns><c>true</c> if assignable; otherwise <c>false</c>.</returns>
        public static bool IsAssignableFromGenericType(this Type self, Type? type) {
            if (type == null) { return false; }

            foreach (var item in type.GetInterfaces()) {
                if (item.IsGenericType && item.GetGenericTypeDefinition() == self) {
                    return true;
                }
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == self) {
                return true;
            }

            return IsAssignableFromGenericType(self, type.BaseType);
        }

        #endregion
    }
}
