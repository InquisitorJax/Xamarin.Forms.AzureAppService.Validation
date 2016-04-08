using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Eventing;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Validation.Client.XForms.Custom
{
    public class MobileClientFake : IMobileServiceClient
    {
        public Uri AlternateLoginHost { get; set; }

        public MobileServiceUser CurrentUser { get; set; }

        public IMobileServiceEventManager EventManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string InstallationId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string LoginUriPrefix { get; set; }

        public Uri MobileAppUri
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public MobileServiceJsonSerializerSettings SerializerSettings { get; set; }

        public IMobileServiceSyncContext SyncContext
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IMobileServiceSyncTable GetSyncTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public IMobileServiceSyncTable<T> GetSyncTable<T>()
        {
            throw new NotImplementedException();
        }

        public IMobileServiceTable GetTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public IMobileServiceTable<T> GetTable<T>()
        {
            throw new NotImplementedException();
        }

        public Task<JToken> InvokeApiAsync(string apiName, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<JToken> InvokeApiAsync(string apiName, JToken body, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<JToken> InvokeApiAsync(string apiName, HttpMethod method, IDictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<JToken> InvokeApiAsync(string apiName, JToken body, HttpMethod method, IDictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> InvokeApiAsync(string apiName, HttpContent content, HttpMethod method, IDictionary<string, string> requestHeaders, IDictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<T> InvokeApiAsync<T>(string apiName, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<T> InvokeApiAsync<T>(string apiName, HttpMethod method, IDictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<U> InvokeApiAsync<T, U>(string apiName, T body, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<U> InvokeApiAsync<T, U>(string apiName, T body, HttpMethod method, IDictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<MobileServiceUser> LoginAsync(string provider, JObject token)
        {
            throw new NotImplementedException();
        }

        public Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, JObject token)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}