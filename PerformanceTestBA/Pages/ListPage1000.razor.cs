using Microsoft.AspNetCore.Components;
using PerformanceTestBA.Shared.Models;
using PerformanceTestBA.Shared.Services;

namespace PerformanceTestBA.Pages;

public partial class ListPage1000
{
    [Inject]
    public ICarService CarService { get; set; } = null!; 
    
    private List<Car>? _cars;
    private bool _sortAscendingBrand;
    private bool _allColorsSet;
    private bool _carsFilteredByBrand;
    private int _carsAmount;

    protected override async Task OnInitializedAsync()
    {
        _cars = await CarService.GetCars(1000);
    }

    private void ToggleSortByBrand()
    {
        if (_cars == null) return;
        _cars = _sortAscendingBrand ? _cars.OrderBy(car => car.Brand).ToList() : _cars.OrderByDescending(car => car.Brand).ToList();

        _sortAscendingBrand = !_sortAscendingBrand;
        _carsFilteredByBrand = !_carsFilteredByBrand;
    }
    
    private void SetAllColorsToRed()
    {
        if (_cars == null) return;
        
        foreach (var car in _cars)
        {
            car.Color = "Red";
        }

        StateHasChanged();
        _allColorsSet = true;
    }
    
    private async Task GetMoreCars(int amount)
    {
        var moreCars = await CarService.GetCars(amount);
        _cars?.AddRange(moreCars);
        _carsAmount += amount;
    }
    
    private void DeleteAddedCarsWith()
    {
        _cars?.RemoveRange(_cars.Count - _carsAmount, _carsAmount);
        _carsAmount = 0;
    }
}