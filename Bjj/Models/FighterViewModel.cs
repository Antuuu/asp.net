using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bjj.Models;

public class FighterViewModel
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Name")]
    [StringLength(50)]
    [DataType(DataType.Text)]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last name")]
    [StringLength(50)]
    [DataType(DataType.Text)]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "Date of birth")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Display(Name = "Weight category")]
    public WeightClasses WeightCategory { get; set; }

    [Required] [Display(Name = "Belt colour")] public BeltColours BeltColour { get; set; }
    /* do uzupełnienia */
    
    public string Academy { get; set; }
    public int AcademyId { get; set; }

    
    public IEnumerable<SelectListItem>? Academies { get; set; }


}