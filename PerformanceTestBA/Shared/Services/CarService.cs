using Bogus;
using PerformanceTestBA.Shared.Models;

namespace PerformanceTestBA.Shared.Services;

public class CarService : ICarService
{
    public async Task<List<Car>?> GetAll()
    {
        return await GenerateCars();
    }

    private Task<List<Car>> GenerateCars()
    {
        var brands = new[] { "Toyota", "Ford", "BMW", "Tesla", "Chevrolet", "Honda" };
        var colors = new[] { "Red", "Blue", "Black", "White", "Silver", "Green" };
        var models = new Dictionary<string, string[]> {
            { "Toyota", new[] {"Corolla", "Camry", "RAV4"} },
            { "Ford", new[] {"Fiesta", "Focus", "Mustang"} },
            { "BMW", new[] {"320", "X5", "M3"} },
            { "Tesla", new[] {"Model S", "Model 3", "Model X"} },
            { "Chevrolet", new[] {"Impala", "Camaro", "Silverado"} },
            { "Honda", new[] {"Civic", "Accord", "CR-V"} }
        };
        
        var carFaker = new Faker<Car>()
            .RuleFor(c => c.Brand, f => f.PickRandom(brands))
            .RuleFor(c => c.Model, (f, c) => f.PickRandom(models[c.Brand]))
            .RuleFor(c => c.Color, f => f.PickRandom(colors))
            .RuleFor(c => c.FuelType, f => f.PickRandom<FuelTypeEnum>())
            .RuleFor(c => c.ReleaseYear, f => f.Random.Int(1990, 2024));

        return Task.FromResult(carFaker.Generate(1000));
    }
}