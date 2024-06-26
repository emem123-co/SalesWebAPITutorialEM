using System.ComponentModel.DataAnnotations;

namespace SalesWebAPITutorialEM.Models;

public class Employee
{
    public int Id { get; set; }

    [StringLength(50)]
    public string email { get; set; } = string.Empty;

    [StringLength(20)]
    public string Password { get; set; } = string.Empty;

}
