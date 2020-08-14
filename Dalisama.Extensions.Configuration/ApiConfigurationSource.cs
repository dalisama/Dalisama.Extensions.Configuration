using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Dalisama.Extensions.Configuration
{
    public class ApiConfigurationSource : IConfigurationSource
    {

        public ApiConfigurationOptions ApiOption;

        public ApiConfigurationSource(ApiConfigurationOptions apiOption)
        {
            ApiOption = apiOption;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ApiConfigurationProvider(this);
        }
    }

    public class ApiConfigurationOptions
    {
        public string Url { get; set; }
        public bool ReloadOnChange { get; set; }
        public int ReloadDelay { get; set; } = 500;
        public Func<HttpClient> HttpClientFactory { get; set; }
        public Func<string, string, string> COnfigKeyFormatter { get; set; } = (key, value) => key;
        public Func<string, string, string> COnfigValueFormatter { get; set; } = (key, value) => value;
    }


}
