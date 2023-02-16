using Bjj.Data;
using Bjj.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bjj.Controllers
{
    public class FightController : Controller
    {
        private readonly AppDbContext _context;

        public FightController(AppDbContext context)
        {
            _context = context;
        }

        private List<Fight> _fights { get; set; }
        private List<Fighter> _fighters { get; set; }
        private List<FightResultBy> _fightFinishes { get; set; }

        public IActionResult Index()
        {
            _fights = _context.Fights.Include(f => f.Fighter1).Include(f => f.Fighter2)
                .Include(figter => figter.Winner).Include(fightresultby => fightresultby.FightResultBy).ToList();
            return View("Index", _fights);
        }

        [HttpGet]
        public IActionResult Add()
        {
            _fighters = _context.Fighters.ToList();
            _fightFinishes = _context.FightResultsBy.ToList();

            var mockFight = new FightViewModel
            {
                DateOfFight = new DateTime(1998,
                    9,
                    20),
                WeightCategory = (WeightClasses) 0,
                Fighter1Id = 0,
                Fighter2Id = 0,
                WinnerId = 0,
                FightResultById = 0,
                Fighters = _fighters.Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id.ToString()
                }).ToList(),
                FightEndBy = _fightFinishes.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),

            };
            return View(mockFight);
        }
        
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            _fighters = _context.Fighters.ToList();
            _fightFinishes = _context.FightResultsBy.ToList();
            _fights = _context.Fights.ToList();
            
            if (id == null)
            {
                return NotFound();
            }

            var fight =  _context.Fights.FirstOrDefault(fight1 => fight1.Id == id);
            if (fight == null)
            {
                return NotFound();
            }

            var mockFight = new FightViewModel
            {
                Id = fight.Id,
                DateOfFight = fight.DateOfFight,
                WeightCategory = fight.WeightCategory,
                Fighter1Id = (int) fight.Fighter1Id,
                Fighter2Id = (int) fight.Fighter2Id,
                WinnerId = (int) fight.WinnerId,
                FightResultById = (int) fight.FightResultById,
                Fighters = _fighters.Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id.ToString()
                }).ToList(),
                FightEndBy = _fightFinishes.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),

            };
            return View(mockFight);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [Bind("Id,Fighter1Id,Fighter2Id,WinnerId,DateOfFight,WeightCategory,FightResultById")]
            FightViewModel fightViewModel)
        {
            
            _fighters = _context.Fighters.ToList();
            _fightFinishes = _context.FightResultsBy.ToList();
            _fights = _context.Fights.ToList();

            var fight =  _context.Fights.FirstOrDefault(fight1 => fight1.Id == fightViewModel.Id);

            fight.Fighter1Id = fightViewModel.Fighter1Id;
            fight.Fighter2Id = fightViewModel.Fighter2Id;
            fight.WinnerId = fightViewModel.WinnerId;
            fight.DateOfFight = fightViewModel.DateOfFight;
            fight.WeightCategory = fightViewModel.WeightCategory;
            fight.FightResultById = fightViewModel.FightResultById;

            
            _context.Fights.Update(fight);
            _context.SaveChangesAsync();
            return Index();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(
            [Bind("Fighter1Id,Fighter2Id,WinnerId,DateOfFight,WeightCategory,FightResultById")]
            FightViewModel fightViewModel)
        {
            
            _fighters = _context.Fighters.ToList();
            _fightFinishes = _context.FightResultsBy.ToList();
            
          
            var fight = new Fight
            {
                Fighter1Id = fightViewModel.Fighter1Id,
                Fighter2Id= fightViewModel.Fighter2Id,
                WinnerId = fightViewModel.WinnerId,
                DateOfFight = fightViewModel.DateOfFight,
                WeightCategory = fightViewModel.WeightCategory,
                FightResultById = fightViewModel.FightResultById
            };
            _context.Fights.Add(fight);
            await _context.SaveChangesAsync();
            return Index();
        }
        
        public IActionResult Delete(Fight fight)
        {

            _context.Fights.Remove(fight);
            _context.SaveChangesAsync();

            return Index();
        }

    }
}