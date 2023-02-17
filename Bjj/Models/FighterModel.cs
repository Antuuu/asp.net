using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Bjj.Models;

public class Fighter
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    
    [Required]
    [Display(Name = "Belt colour")]
    public BeltColours BeltColour { get; set; }
    /* do uzupełnienia */
    
    public Academy? FAcademy { get; set; }
    
    public int? FAcademyId { get; set; }
    /* do uzupełnienia */
    

    [NotMapped]
    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }
}
public enum BeltColours 
{
    White = 1,
    Blue = 2,
    Purple = 3,
    Brown = 4,
    Black = 5
}

public enum WeightClasses 
{
    Feather = 1,
    Light = 2,
    Middle = 3,
    Heavy = 4,
    Superheavy = 5
}