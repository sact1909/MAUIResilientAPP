using Polly;
using Refit;
using Polly.Fallback;
using Polly.Wrap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace MAUIResilientAPP.APISettings
{
    public class PolicyBuilder<T>
    {
        public AsyncPolicyWrap<TResult> Build<TResult>()
        {
            return AuthorizationPolicy<TResult>().WrapAsync(RetryPolicy());

        }

        public AsyncPolicyWrap Build()
        {
            return Policy.WrapAsync(AuthorizationPolicy(), RetryPolicy());
        }

        private AsyncFallbackPolicy<TResult> AuthorizationPolicy<TResult>()
        {
            return Policy<TResult>.Handle<ApiException>()
                .FallbackAsync((token) =>
                {
                    Debug.WriteLine("Invalid credentials");

                    return Task.FromResult(default(TResult));
                });
        }

        private AsyncFallbackPolicy AuthorizationPolicy()
        {
            return Policy.Handle<ApiException>()
                .FallbackAsync((token) =>
                {
                    Debug.WriteLine("Invalid credentials");

                    return Task.CompletedTask;
                });
        }

        private AsyncPolicy RetryPolicy()
        {
            var numberOfRetries = 3;

            return Policy.Handle<ApiException>(a=>a.StatusCode != HttpStatusCode.Unauthorized  && a.StatusCode != HttpStatusCode.NotFound).WaitAndRetryAsync(
                numberOfRetries,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2.5, retryAttempt)),
                (exception, timeSpan, attempt, context) =>
                {
                    Debug.WriteLine("=================== Exception ==========================");
                    Debug.WriteLine($"Retry attempt {attempt} of {numberOfRetries}");
                    Debug.WriteLine(string.Empty);
                    Debug.WriteLine(($"Exception: {exception}."));
                    Debug.WriteLine("========================================================");
                });
        }
    }
}
