using DryIoc.ImTools;
using MAUIResilientAPP.APISettings;
using Microsoft.Maui.Controls.Shapes;
using Polly;
using Refit;
using System.Diagnostics;

namespace MAUIResilientAPP.ViewModels;

public class MainPageViewModel : BindableBase
{

    IBackendClient<ApiMethodCollection> _api;

    private bool _isLoading;
    public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }



    public string Title => "Main Page";
    public DelegateCommand BasicMethodCommand { get; }

    public MainPageViewModel(IBackendClient<ApiMethodCollection> api)
    {
        _api = api;
        BasicMethodCommand = new DelegateCommand(async() => await OnBasicMethodAuthExecuted());
    }

    

    private async Task OnBasicMethodExecuted()
    {
 
        var result = await _api.CallAsync(a=>a.GetNormalString());
        Debug.WriteLine(result);
    }

    private async Task OnBasicMethodBadRequestExecuted()
    {
        await _api.CallAsync(a => a.GetBadRequest());
    }

    private async Task OnBasicMethodAuthExecuted()
    {
        var result = await _api.CallAsync(a => a.GetStringWithAuth());
        Debug.WriteLine(result);
    }

   

}
