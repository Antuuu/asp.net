using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Bjj.Models;

public class FightResultBy
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [Display(Name = "Nazwa")]
    [StringLength(50)]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
/* do uzupełnienia */
}


