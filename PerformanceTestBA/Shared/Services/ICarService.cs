using PerformanceTestBA.Shared.Models;

namespace PerformanceTestBA.Shared.Services;

public interface ICarService
{ 
    Task<List<Car>> GetCars(int amount);
}