using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesWebAPITutorialEM.Models;

public class Customer
{
    public int Id { get; set; }

    [StringLength(30)]
    public string Name { get; set; } = string.Empty;

    [StringLength(30)]
    public string City { get; set; } = string.Empty;

    [StringLength(30)]
    public string State { get; set; } = string.Empty;

    [Column(TypeName = "decimal (12,2)")]
    public decimal Sales { get; set; }

    [Column(TypeName = "bit")]
    public bool Active { get; set; }
}

    //Property("Active").IsRequired(); this is the fluent-api way to determine a value is not nullable.