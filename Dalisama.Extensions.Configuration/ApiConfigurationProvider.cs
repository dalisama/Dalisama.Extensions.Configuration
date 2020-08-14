using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Timers;

namespace Dalisama.Extensions.Configuration
{
    class ApiConfigurationProvider : ConfigurationProvider
    {
        public ApiConfigurationSource Source { get; }

        public ApiConfigurationProvider(ApiConfigurationSource source)
        {
            Source = source;
            if (source.ApiOption.ReloadOnChange)
            {
                var aTimer = new Timer(source.ApiOption.ReloadDelay);
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Start();
            }
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Load();
        }

        public override void Load()
        {

            using (var client = Source.ApiOption.HttpClientFactory.Invoke())
            {
                var result = client.GetAsync(Source.ApiOption.Url).GetAwaiter().GetResult();
                if (result.IsSuccessStatusCode)
                {
                    Data.Clear();
                    var content = result.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                    foreach (var item in data)
                    {
                        Set(
                            Source.ApiOption.COnfigKeyFormatter(item.Key, item.Value)
                            , Source.ApiOption.COnfigValueFormatter(item.Key, item.Value)
                            );
                    }
                }
            }
        }
    }
}
