using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIResilientAPP.APISettings
{
    public interface IBackendClient<T>
    {
        Task<TResult> CallAsync<TResult>(Func<T, Task<TResult>> apiCall);

        Task CallAsync(Func<T, Task> apiCall);

    }
}
