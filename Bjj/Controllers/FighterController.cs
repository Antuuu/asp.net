using System.Dynamic;
using System.Linq;
using Bjj.Data;
using Microsoft.AspNetCore.Mvc;
using Bjj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using SQLitePCL;

namespace Bjj.Controllers
{
    public class FighterController : Controller
    {
        private readonly AppDbContext _context;
        public FighterController(AppDbContext context)
        {
            _context = context;
        }
        
        private List<Fighter> _fighters;
        
        public IActionResult Index()
        {
            _fighters = _context.Fighters.ToList();
            return View("Index", _fighters);
        }
        
        public IActionResult Delete(Fighter fighter)
        {

            _context.Fighters.Remove(fighter);
            _context.SaveChangesAsync();

            return Index();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth,WeightCategory,BeltColour")] Fighter fighter)
        {
            if (id != fighter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(fighter);
                await _context.SaveChangesAsync();
            }
            return Index();
        }        
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fighter = await _context.Fighters.FindAsync(id);
            if (fighter == null)
            {
                return NotFound();
            }
            return View(fighter);
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }  
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,FirstName,LastName,DateOfBirth,WeightCategory,BeltColour")] Fighter fighter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fighter);
                await _context.SaveChangesAsync();
            }
            return Index();
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            var fighter =  _context.Fighters.Find(id);
            if (fighter == null)
            {
                return NotFound();
            }

            string result;
            string oponent;
            var _fights = _context.Fights.Include(fight => fight.Fighter1).Include(fight => fight.Fighter2).Include(fight => fight.Winner).Include(fight => fight.FightResultBy).Where(fight => (fight.Fighter1.Id == id || fight.Fighter2.Id == id));
            var fightsToDisplay = new List<FightDisplay>();
            foreach (var fight in _fights)
            {
                if (fighter.Id == fight.Fighter1.Id)
                {
                    oponent = fight.Fighter2.FirstName + " " + fight.Fighter2.LastName;
                }
                else
                {
                    oponent = fight.Fighter1.FirstName + " " + fight.Fighter1.LastName;
                }
                result = fighter.Id == fight.Winner.Id ? "Win" : "Lose";
                fightsToDisplay.Add(new FightDisplay{ Oponent = oponent, Result = result, DateOfFight = fight.DateOfFight, FightResultBy = fight.FightResultBy.Name});
            }
            var winnedFigtsFinishes = from f in _fights
                where id == f.Winner.Id
                group f by f.FightResultBy
                into submissionCount
                select new
                {
                    label = submissionCount.Key.Name,
                    y = submissionCount.Count(),
                };
            ViewBag.Data = JsonConvert.SerializeObject(winnedFigtsFinishes);
                
            ViewData["Fights"] = fightsToDisplay;
            return View(fighter);
        }
    }
}