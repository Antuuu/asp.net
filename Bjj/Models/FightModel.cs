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

    [Key]
    public int IdFigter1 { get; set; }
    
    [Key]
    public int IdFigter2 { get; set; }
    
    [Required]
    [Display(Name = "Data walki")]
    [DataType(DataType.Date)]
    public DateTime DateOfFight { get; set; }

    [Required]
    [Display(Name = "Kategoria Wagowa")]
    public WeightClasses WeightCategory { get; set; }

    [Required] [Display(Name = "Pas")] public BeltColours BeltColour { get; set; }
    /* do uzupełnienia */
}