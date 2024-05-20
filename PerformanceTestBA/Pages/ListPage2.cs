using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using PerformanceTestBA.Shared.Models;
using PerformanceTestBA.Shared.Services;

namespace PerformanceTestBA.Pages;

[Route("/listPage33")]
public class ListPage2 : ComponentBase
{
  [Inject]
  public ICarService CarService { get; set; } = null!;

  [Inject]
  public NavigationManager NavigationManager { get; set; } = null!;

  private List<Car>? _cars;
  private bool _sortAscendingYear;
  private bool _sortAscendingBrand;

  protected override async Task OnInitializedAsync()
  {
    _cars = await CarService.GetAll();
  }
    
  private void SortByReleaseYear()
  {
    if (_cars == null) return;
        
    _cars = _sortAscendingYear ? _cars.OrderBy(car => car.ReleaseYear).ToList() : _cars.OrderByDescending(car => car.ReleaseYear).ToList();

    _sortAscendingYear = !_sortAscendingYear;
  }
    
  private void ToggleSortByBrand()
  {
    if (_cars == null) return;
    _cars = _sortAscendingBrand ? _cars.OrderBy(car => car.Brand).ToList() : _cars.OrderByDescending(car => car.Brand).ToList();

    _sortAscendingBrand = !_sortAscendingBrand;
  }
    
  private void SetAllColorsToRed()
  {
    if (_cars == null) return;
        
    foreach (var car in _cars)
    {
      car.Color = "Chroma";
    }

    StateHasChanged();
  }
  
  private Stopwatch _stopwatch = new Stopwatch();
  
  
   protected override void BuildRenderTree(
    #nullable disable
    RenderTreeBuilder __builder)
    {
      _stopwatch.Start();
      

      __builder.AddMarkupContent(3, "\n\n");
      __builder.AddMarkupContent(4, "<h1>List Page</h1>");
      if (!this._cars.Any<Car>())
        return;
      __builder.OpenElement(5, "button");
      __builder.AddAttribute<MouseEventArgs>(6, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object) this, new Action(this.SetAllColorsToRed)));
      __builder.AddContent(7, "Set All Colors to Red");
      __builder.CloseElement();
      __builder.AddMarkupContent(8, "\n");
      __builder.OpenElement(9, "table");
      __builder.AddAttribute(10, "class", "table");
      __builder.OpenElement(11, "thead");
      __builder.OpenElement(12, "tr");
      __builder.AddMarkupContent(13, "<th>Model</th>\n        ");
      __builder.OpenElement(14, "th");
      __builder.OpenElement(15, "button");
      __builder.AddAttribute<MouseEventArgs>(16, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object) this, (Action) (() => this.ToggleSortByBrand())));
      __builder.AddContent(17, "Brand");
      __builder.CloseElement();
      __builder.CloseElement();
      __builder.AddMarkupContent(18, "\n        ");
      __builder.AddMarkupContent(19, "<th>Color</th>\n        ");
      __builder.AddMarkupContent(20, "<th>Fuel Type</th>\n        ");
      __builder.OpenElement(21, "th");
      __builder.OpenElement(22, "button");
      __builder.AddAttribute<MouseEventArgs>(23, "onclick", EventCallback.Factory.Create<MouseEventArgs>((object) this, (Action) (() => this.SortByReleaseYear())));
      __builder.AddContent(24, "Release Year");
      __builder.CloseElement();
      __builder.CloseElement();
      __builder.CloseElement();
      __builder.CloseElement();
      __builder.AddMarkupContent(25, "\n    ");
      __builder.OpenElement(26, "tbody");

      foreach (var car in _cars)
      {
        __builder.OpenElement(30, "tr");
        __builder.OpenElement(31, "td");
        __builder.AddContent(32, car.Model);
        __builder.CloseElement();
        __builder.AddMarkupContent(33, "\n            ");
        __builder.OpenElement(34, "td");
        __builder.AddContent(35, car.Brand);
        __builder.CloseElement();
        __builder.AddMarkupContent(36, "\n            ");
        __builder.OpenElement(37, "td");
        __builder.AddContent(38, car.Color);
        __builder.CloseElement();
        __builder.AddMarkupContent(39, "\n            ");
        __builder.OpenElement(40, "td");
        __builder.AddContent(41, (object) car.FuelType);
        __builder.CloseElement();
        __builder.AddMarkupContent(42, "\n            ");
        __builder.OpenElement(43, "td");
        __builder.AddContent(44, (object) car.ReleaseYear);
        __builder.CloseElement();
        __builder.CloseElement();
      }
      
      __builder.CloseElement();
      __builder.CloseElement();
      
      _stopwatch.Stop();
      
      Console.WriteLine($"Component took {_stopwatch.Elapsed:c}");
      
      DemoStorage.Times.Add(_stopwatch.Elapsed);

      if (DemoStorage.Times.Count < 100)
      {
        NavigationManager.NavigateTo("/redirectToListPage");
      }
      else
      {
        Console.WriteLine("D O N E !");
        
        NavigationManager.NavigateTo("/resultsPage");
      }
    }
}