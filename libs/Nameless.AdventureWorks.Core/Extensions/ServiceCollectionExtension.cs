using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nameless.AdventureWorks {
    public static class ServiceCollectionExtension {
        #region Public Static Methods

        /// <summary>
        /// Fetchs the <typeparamref name="TOptions"/> from configuration file and
        /// adds it as a singleton services in the dependency container.
        /// </summary>
        /// <typeparam name="TOptions">Type of the options.</typeparam>
        /// <param name="self">The service collection.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> instance.</param>
        /// <param name="factory">A factory for the <typeparamref name="TOptions"/></param>
        /// <returns>A reference to the <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection FetchConfiguration<TOptions>(this IServiceCollection self, IConfiguration configuration, Func<TOptions> factory) where TOptions : class {
            Prevent.Against.Null(configuration, nameof(configuration));
            Prevent.Against.Null(factory, nameof(factory));

            var opts = factory();
            var key = typeof(TOptions).Name.TrimEnd("Options", "Settings");
            configuration.GetSection(key).Bind(opts);
            self.AddSingleton(opts);
            return self;
        }
        /// <summary>
        /// Fetchs the <typeparamref name="TOptions"/> from configuration file and
        /// adds it as a singleton services in the dependency container. The
        /// <typeparamref name="TOptions"/> must have a parameterless constructor.
        /// </summary>
        /// <typeparam name="TOptions">Type of the options.</typeparam>
        /// <param name="self">The service collection.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> instance.</param>
        /// <returns>A reference to the <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection FetchConfiguration<TOptions>(this IServiceCollection self, IConfiguration configuration) where TOptions : class, new()
            => FetchConfiguration(self, configuration, () => new TOptions());

        #endregion
    }
}
