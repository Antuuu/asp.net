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
    
    [Route("api/[controller]")]
    [ApiController]
    public class FighterApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FighterApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        [Produces("application/json")] 
        public Fighter Get(int id)
        {
            return _context.Fighters.SingleOrDefault(f => f.Id == id) ?? throw new InvalidOperationException();
        }
        
        [HttpPost]
        public ActionResult<Fighter> Add([FromBody] Fighter fighter)
        {
            if(ModelState.IsValid)
            {
                _context.Fighters.Add(fighter);
                _context.SaveChanges();
                return Created($"/api/fighterapi/{fighter.Id}", fighter);
            }
            return BadRequest(ModelState);
        }

    }
}