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
                .Include(figter => figter.Winner).ToList();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(
            [Bind("Fighter1Id,Fighter2Id,WinnerId,DateOfFight,WeightCategory,FightResultByID")]
            FightViewModel fightViewModel)
        {
            
            _fighters = _context.Fighters.ToList();
            _fightFinishes = _context.FightResultsBy.ToList();
            
            var fighter1 = _fighters.Where(f => f.Id == fightViewModel.Fighter1Id).FirstOrDefault();
            var fighter2 = _fighters.Where(f => f.Id == fightViewModel.Fighter2Id).FirstOrDefault();
            var winner = _fighters.Where(f => f.Id == fightViewModel.WinnerId).FirstOrDefault();
            var fightFinishedBy = _fightFinishes.Where(f => f.Id == fightViewModel.FightResultById).FirstOrDefault();

            var fight = new Fight
            {
                Fighter1 = fighter1,
                Fighter2 = fighter2,
                Winner = winner,
                DateOfFight = fightViewModel.DateOfFight,
                WeightCategory = fightViewModel.WeightCategory,
                FightResultBy = fightFinishedBy,
            };
            _context.Fights.Add(fight);
            _context.SaveChanges();
            return View();
        }

    }
}