using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebAPITutorialEM.Data;
using SalesWebAPITutorialEM.Models;


namespace SalesWebAPITutorialEM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppdBContext _context;

        public OrdersController(AppdBContext context)
        {
            _context = context;
        }

    //GET all orders:api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Orders.ToListAsync();
        }
    //GET orders by status: api/Orders/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByStatus(string status)
        {
            return await _context.Orders.Where(x => x.Status == status).ToListAsync();
        }
    //GET orders by ID: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

    //PUT shipped status on orders: api/Orders/shipped/5
        [HttpPut("shipped/{id}")] //need a different URL for our new PUT method. This makes the shipped status update method unique from the base PUT method. 
        public async Task<IActionResult> ShippedOrder(int id, Order order)
        {
            order.Status = "SHIPPED";
            return await PutOrder(id, order);//calling the PutOrder method already created below, passing through the same parameters we recieved in the ShippedOrder method. Asking the ShippedOrder method to return whatever the PutOrder method returns. Added "await" because it is an async method. 
        }
    //PUT SUM into Total: api/Orders/total/{id}
        [HttpPut("total/{id}")]
        public async Task<IActionResult> PutOrder(OrderLine orderline, Item item, Order order)
        {
        var orderJoin = from oL in _context.OrderLines
	                    join o in _context.Orders on oL.OrderId equals o.Id
	                    join i in _context.Items on oL.ItemId equals i.Id
                        select new
                        {
                            oL.Quantity,
                            o.Id,
                            i.ItemPrice
                        };
        
        decimal orderTotal =+ (Convert.ToDecimal(orderline.Quantity) * item.ItemPrice);
        
        order.Total = orderTotal;
        
        await _context.SaveChangesAsync();

        //return orderTotal = order.Total;

        }
    
    //PUT new info using order ID: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

    //POST new orders with NEW status: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            order.Status = "NEW"; //this will make the status column for new orders first.
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order); //instance of an order
        }

    //DELETE orders by ID: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
