using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesWebAPITutorialEM.Models;

public class Item
{
    public int Id { get; set; }

    [StringLength(30)]
    public string ItemDescription { get; set; } = string.Empty;

    [Column(TypeName = "decimal(7,2)")] //typename is a string that tells SQL the column sttirubute.
    public decimal ItemPrice { get; set; }

}
