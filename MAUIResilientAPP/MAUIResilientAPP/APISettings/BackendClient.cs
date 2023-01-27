using MAUIResilientAPP.APISettings.Attributes;
using Polly;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIResilientAPP.APISettings
{
    public class BackendClient<T> : IBackendClient<T>
    {
        private readonly string _apiBaseAddress;
        private readonly PolicyBuilder<T> _policy;

        public BackendClient()
        {
            _policy = new PolicyBuilder<T>();
            _apiBaseAddress = GetUrl();
        }

        public async Task<TResult> CallAsync<TResult>(Func<T, Task<TResult>> apiCall)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet) return default(TResult);

            var client = CreateClient();

            return await _policy.Build<TResult>().ExecuteAsync(async () => await apiCall(client));
        }

        public async Task CallAsync(Func<T, Task> apiCall)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet) return;

            var client = CreateClient();

            await _policy.Build().ExecuteAsync(async () => await apiCall(client));
        }

        private T CreateClient(string apiBaseAddress = null)
        {
            var service = RestService.For<T>(apiBaseAddress ?? _apiBaseAddress);
            return service;
        }

        private string GetUrl()
        {
            var urlAttribute = (UrlAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(UrlAttribute));

            return urlAttribute.Url;
        }
    }
}
