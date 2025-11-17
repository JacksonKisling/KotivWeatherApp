using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace KotivWeatherApp.Tests.TestHelpers
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        // A delegate the test can assign to define the behavior
        public Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>? HandlerFunc { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (HandlerFunc != null)
            {
                return HandlerFunc(request, cancellationToken);
            }

            // Default response if not overridden
            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
        }
    }
}
