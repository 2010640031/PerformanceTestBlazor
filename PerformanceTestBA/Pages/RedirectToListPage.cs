using Microsoft.AspNetCore.Components;

namespace PerformanceTestBA.Pages;

[Route("/redirectToListPage")]
public class RedirectToListPage : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        
        NavigationManager.NavigateTo("/listPage");
    }
}