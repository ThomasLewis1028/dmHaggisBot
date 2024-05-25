using Microsoft.EntityFrameworkCore;
using SWNUniverseGenerator.CreationTools;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.DefaultSettings;
using SWNUniverseGenerator.Models;

namespace SWNBlazorApp.Data;

public class ShipService : DataService<ShipService>
{
    public ShipService(UniverseContext context, ILogger<ShipService> logger) : base(context, logger)
    {

    }
    
    public Task<Ship> GetShipAsync(string shipId)
    {
        Ship result;
        var repo = new Repository<Ship>(_context);
        result = repo.GetById(shipId);
        
        return Task.FromResult(result);
    }
    
    public Task<List<Ship>> GetShipsAsync(string universeId)
    {
        List<Ship> result;
        var repo = new Repository<Ship>(_context);
        var entityList = repo.Search(c => c.UniverseId == universeId).ToList();
        result = entityList.Cast<Ship>().ToList();
    
        return Task.FromResult(result);
    }

    public Task<Hull> GetShipHull(string hullId)
    {
        var repo = new Repository<Hull>(_context);
        var result = repo.GetById(hullId);

        return Task.FromResult(result);
    }

    public Task<List<Armament>> GetShipArmaments(string shipId)
    {
        List<Armament> armamentList = new ();
        var shipArmaRepo = new Repository<ShipArmament>(_context);
        var armaRepo = new Repository<Armament>(_context);
        var result = shipArmaRepo.Search(a => a.ShipId == shipId).ToList();

        foreach (var entity in result)
        {
            var shipArmament = (ShipArmament)entity;
            armamentList.Add(armaRepo.GetById(shipArmament.ArmamentId));
        }
        
        return Task.FromResult(armamentList);
    }

    public Task<List<Defense>> GetShipDefenses(string shipId)
    {
        List<Defense> armamentList = new ();
        var shipArmaRepo = new Repository<ShipDefense>(_context);
        var armaRepo = new Repository<Defense>(_context);
        var result = shipArmaRepo.Search(a => a.ShipId == shipId).ToList();

        foreach (var entity in result)
        {
            var shipArmament = (ShipDefense)entity;
            armamentList.Add(armaRepo.GetById(shipArmament.DefenseId));
        }
        
        return Task.FromResult(armamentList);
    }

    public Task<List<Fitting>> GetShipFittings(string shipId)
    {
        List<Fitting> armamentList = new ();
        var shipArmaRepo = new Repository<ShipFitting>(_context);
        var armaRepo = new Repository<Fitting>(_context);
        var result = shipArmaRepo.Search(a => a.ShipId == shipId).ToList();

        foreach (var entity in result)
        {
            var shipArmament = (ShipFitting)entity;
            armamentList.Add(armaRepo.GetById(shipArmament.FittingId));
        }
        
        return Task.FromResult(armamentList);
    }

    public Task<int> GetShipArmamentCount(string shipId)
    {
        var repo = new Repository<ShipArmament>(_context);
        var result = repo.Count(a => a.ShipId == shipId);
        return Task.FromResult(result);
    }
    
    public Task<int> GetShipDefenseCount(string shipId)
    {
        var repo = new Repository<ShipDefense>(_context);
        var result = repo.Count(a => a.ShipId == shipId);
        return Task.FromResult(result);
    }
    
    public Task<int> GetShipFittingCount(string shipId)
    {
        var repo = new Repository<ShipFitting>(_context);
        var result = repo.Count(a => a.ShipId == shipId);
        return Task.FromResult(result);
    }
    
    public void CreateShip(ShipDefaultSettings defaultSettings)
    {
        Creation creation = new Creation(_context);
        creation.CreateShips(defaultSettings);
    }
}