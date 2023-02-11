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

        private List<Fighter> _fighters;

        public IActionResult Index()
        {
            _fighters = _context.Fighters.ToList();
            return View("Index", _fighters);
        }
    }
}