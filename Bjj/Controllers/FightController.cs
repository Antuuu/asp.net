using Bjj.Data;
using Bjj.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bjj.Controllers;

public class FightController : Controller
{
    private readonly AppDbContext _context;
    public FightController(AppDbContext context)
    {
        _context = context;
        
    }
        
    private List<Fight> _fights;
    private List<Fighter> _fighters;
    private List<FightResultBy> _fightFinishes;
        
    public IActionResult Index()
    {
        _fights = _context.Fights.Include(figter => figter.Fighter1).Include(figter => figter.Fighter2).Include(figter => figter.Winner).ToList();
        return View("Index", _fights);
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        _fighters = _context.Fighters.ToList();
        _fightFinishes = _context.FightResultsBy.ToList();
        var mockFight = new Fight
        {
            DateOfFight = new DateTime(1998, 9, 20),
            Fighter1 = null,
            Fighter2 = null,
            Winner = null,
            FightResultBy = null,
            Fighters = _fighters,
            FightResults = _fightFinishes,
        };
        
        return View(mockFight);
    }  
        
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add([Bind("Id,Fighter1,Fighter2,Winner,DateOfFight,WeightCategory,FightResultBy")] Fight fight)
    {
        
            _context.Fights.Add(fight);
            await _context.SaveChangesAsync();
            return Index();
    }

}