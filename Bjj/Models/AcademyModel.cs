using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Bjj.Models;

public class Academy
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "Name")]
    [StringLength(50)]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    [Required]
    [Display(Name = "Head coach")]
    [StringLength(50)]
    [DataType(DataType.Text)]
    public string HeadCoach { get; set; }
    
    [Required]
    [Display(Name = "Address")]
    [StringLength(100)]
    [DataType(DataType.Text)]
    public string Address { get; set; }
    
/* do uzupełnienia */
}


