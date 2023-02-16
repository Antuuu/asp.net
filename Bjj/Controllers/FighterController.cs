using System.Dynamic;
using System.Linq;
using Bjj.Data;
using Microsoft.AspNetCore.Mvc;
using Bjj.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private List<Academy> _academies;
        
        public IActionResult Index()
        {
            _fighters = _context.Fighters.Include(a => a.FAcademy).ToList();
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth,WeightCategory,BeltColour,AcademyId")] FighterViewModel fighterViewModel)
        {
            if (id != fighterViewModel.Id)
            {
                return NotFound();
            }
            _fighters = _context.Fighters.Include(a => a.FAcademy).ToList();

            var fighter = _fighters.FirstOrDefault(f => f.Id == fighterViewModel.Id);
            fighter.BeltColour = fighterViewModel.BeltColour;
            fighter.FAcademy = _context.Academies.FirstOrDefault(a => a.Id == fighterViewModel.AcademyId);
            fighter.FirstName = fighterViewModel.FirstName;
            fighter.LastName = fighterViewModel.LastName;
            fighter.WeightCategory = fighterViewModel.WeightCategory;
            fighter.DateOfBirth = fighterViewModel.DateOfBirth;
            _context.Update(fighter);
            await _context.SaveChangesAsync();
            return Index();
        }        
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            _academies = _context.Academies.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var fighter =  _context.Fighters.Include(a => a.FAcademy).FirstOrDefault(f => f.Id == id);
            if (fighter == null)
            {
                return NotFound();
            }

            var figterViewModel = new FighterViewModel
            {
                Id = fighter.Id,
                FirstName = fighter.FirstName,
                LastName = fighter.LastName,
                DateOfBirth = fighter.DateOfBirth,
                WeightCategory = fighter.WeightCategory,
                BeltColour = fighter.BeltColour,
                Academy = fighter.FAcademy.Name,
                Academies = _academies.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            
            
            return View(figterViewModel);
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            _academies = _context.Academies.ToList();

            var figterViewModel = new FighterViewModel
            {
                FirstName = null,
                LastName = null,
                DateOfBirth = new DateTime(),
                WeightCategory = WeightClasses.Light,
                BeltColour = BeltColours.White,
                Academies = _academies.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(figterViewModel);
        }  
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,FirstName,LastName,DateOfBirth,WeightCategory,BeltColour,AcademyId")] FighterViewModel fighterViewModel)
        {
            _academies = _context.Academies.ToList();

            
            var fighter = new Fighter
            {
                FirstName = fighterViewModel.FirstName,
                LastName = fighterViewModel.LastName,
                DateOfBirth = fighterViewModel.DateOfBirth,
                WeightCategory = fighterViewModel.WeightCategory,
                BeltColour = fighterViewModel.BeltColour,
                FAcademyId = fighterViewModel.AcademyId
            };
                _context.Add(fighter);
                await _context.SaveChangesAsync();
                return Index();
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            var fighter =  _context.Fighters.Include(a => a.FAcademy).FirstOrDefault(f => f.Id == id);
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
            var loosedFigtsFinishes = from f in _fights
                where id != f.Winner.Id
                group f by f.FightResultBy
                into submissionCount
                select new
                {
                    label = submissionCount.Key.Name,
                    y = submissionCount.Count(),
                };
            ViewBag.Data = JsonConvert.SerializeObject(winnedFigtsFinishes);
            ViewBag.DataLoses = JsonConvert.SerializeObject(loosedFigtsFinishes);

            ViewData["Fights"] = fightsToDisplay;
            return View(fighter);
        }
                
        [HttpGet("{id:int}")]
        public Fighter Get(int id)
        {
            return _context.Fighters.SingleOrDefault(f => f.Id == id);
        }

    }
}