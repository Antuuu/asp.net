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
    public class AcademyController : Controller
    {
        private readonly AppDbContext _context;

        public AcademyController(AppDbContext context)
        {
            _context = context;
        }

        private List<Academy> _academies;

        public IActionResult Index()
        {
            _academies = _context.Academies.ToList();
            return View("Index", _academies);
        }
        
        public IActionResult Delete(Academy academy)
        {

            _context.Academies.Remove(academy);
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
        public async Task<IActionResult> Add([Bind("Id,Name,HeadCoach,Address")] Academy academy)
        {
            _context.Add(academy);
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

            var Academy = await _context.Academies.FindAsync(id);
            if (Academy == null)
            {
                return NotFound();
            }
            return View(Academy);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,HeadCoach,Address")] Academy academy)
        {
            if (id != academy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(academy);
                await _context.SaveChangesAsync();
            }
            return Index();
        }
        
    }
}