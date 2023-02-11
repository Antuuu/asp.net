using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Bjj.Models;

public class Fight
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? Fighter1Id { get; set; }
    public int? Fighter2Id { get; set; }
    public int? WinnerId { get; set; }
    public int? FightResultById { get; set; }
    
    [Display(Name = "First fighter")]
    public Fighter Fighter1 { get; set; }
   
    [Display(Name = "Second fighter")]
    public Fighter Fighter2 { get; set; }
    
    [Display(Name = "Winner")]
    public Fighter Winner { get; set; }

    public FightResultBy FightResultBy { get; set; }
    
    [Required]
    [Display(Name = "Data walki")]
    [DataType(DataType.Date)]
    public DateTime DateOfFight { get; set; }

    [Required]
    [Display(Name = "Kategoria Wagowa")]
    public WeightClasses WeightCategory { get; set; }
    

/* do uzupełnienia */
}

public class FightDisplay
{
    public string Oponent { get; set; }
    public string Result { get; set; }
    public DateTime DateOfFight { get; set; }
    
    public string FightResultBy { get; set; }
}
