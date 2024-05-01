using Microsoft.AspNetCore.Components;
using PerformanceTestBA.Shared.Models;
using PerformanceTestBA.Shared.Services;

namespace PerformanceTestBA.Pages;

public partial class ListPage
{
    [Inject]
    public ICarService CarService { get; set; } = null!; 
    
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
}