using MAUIResilientAPP.APISettings.Attributes;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIResilientAPP.APISettings
{
    [Url("http://10.0.2.2:14303/api")]
    [Headers("Accept: application/json" )]
    public interface ApiMethodCollection
    {
        [Get("/Scenario/GetString")]
        Task<string> GetNormalString();

        [Get("/Scenario/BadRequest")]
        Task GetBadRequest();

        [Get("/Scenario/GetStringWithAuth")]
        Task<string> GetStringWithAuth([Header("ApiKey")] string key = "aaaaaahhhhhhhbbbbbbbbFFFF");
    }
}
