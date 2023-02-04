using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bjj.Models;

public class FightViewModel
{
    [Display(Name = "First fighter")]
    public int Fighter1Id { get; set; }
   
    [Display(Name = "Second fighter")]
    public int Fighter2Id { get; set; }
    
    [Display(Name = "Winner")]
    public int WinnerId { get; set; }

    [Required]
    [Display(Name = "Data walki")]
    [DataType(DataType.Date)]
    public DateTime DateOfFight { get; set; }

    [Required]
    [Display(Name = "Kategoria Wagowa")]
    public WeightClasses WeightCategory { get; set; }

    public int FightResultById { get; set; }
    
    public IEnumerable<SelectListItem>? Fighters { get; set; }
    
    public IEnumerable<SelectListItem>? FightEndBy { get; set; }

/* do uzupełnienia */
}