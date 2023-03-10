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
    public class FightResultByController : Controller
    {
        private readonly AppDbContext _context;

        public FightResultByController(AppDbContext context)
        {
            _context = context;
        }

        private List<FightResultBy> _fightResultBy;

        public IActionResult Index()
        {
            _fightResultBy = _context.FightResultsBy.ToList();
            return View("Index", _fightResultBy);
        }
        
        public IActionResult Delete(FightResultBy fightResultBy)
        {

            _context.FightResultsBy.Remove(fightResultBy);
            _context.SaveChangesAsync();

            return Index();
        }
        

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }  
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Name")] FightResultBy fightResultBy)
        {
            _context.Add(fightResultBy);
            await _context.SaveChangesAsync();
            return Index();
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fightResultBy = await _context.FightResultsBy.FindAsync(id);
            if (fightResultBy == null)
            {
                return NotFound();
            }
            return View(fightResultBy);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FightResultBy fightResultBy)
        {
            if (id != fightResultBy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(fightResultBy);
                await _context.SaveChangesAsync();
            }
            return Index();
        }
        
    }
}