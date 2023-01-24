using System.Linq;
using Bjj.Data;
using Microsoft.AspNetCore.Mvc;
using Bjj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        public async Task<IActionResult> Details(int? id)
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
    }
}