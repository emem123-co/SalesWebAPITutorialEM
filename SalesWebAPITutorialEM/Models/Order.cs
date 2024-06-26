using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesWebAPITutorialEM.Models;

public class Order
{
    [Key] //dont need this because we named PK "Id" but if we dont name it that, we need this here. 
    public int Id { get; set; }

    
    public int? CustomerId { get; set; } //fk, can be null, add ? to type to indicate this property can be null. because we have another table name + id in the property name, this will require the virtual constructor.
    public virtual Customer? Customer { get; set; } //fk constructor

    //no type conversion needed. but if we wanted to anyway, it would be:
    //[Column(TypeName= "DateTime")]
    public DateTime Date { get; set; }

    [Column(TypeName = "decimal(7,2)")]
    public decimal Total { get; set; }
    
    
    [StringLength(30)]
    public string? Description { get; set; } = string.Empty; //can be null, add ? to type to indicate this property can be null

    //NEW, Shipped
    [StringLength(20)]
    public string Status { get; set; } = string.Empty;

}
