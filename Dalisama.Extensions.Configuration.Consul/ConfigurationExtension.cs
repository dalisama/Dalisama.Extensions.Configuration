using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dalisama.Extensions.Configuration.Consul
{
    public static class ConfigurationExtension
    {
        public static IConfigurationBuilder AddApiConfiguration(this IConfigurationBuilder configuration, Action<ApiConfigurationOptions> options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));
            var apiConfigurationOptions = new ApiConfigurationOptions();
            options(apiConfigurationOptions);
            configuration.Add(new ApiConfigurationSource(apiConfigurationOptions));
            return configuration;
        }
    }
}
