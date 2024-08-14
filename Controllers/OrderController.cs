using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var orders = await _context.Orders.ToListAsync();

            if (orders == null)
            {
                return NotFound("Pedido não encontrado");
            }

            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetByIdOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(c => c.id == id);

            if (order == null)
            {
                return NotFound("Pedido não encontrado");
            }

            return Ok(order);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<CreateOrderDTO>> CreateOrder([FromBody]CreateOrderDTO createOrder)
        {
            if (createOrder == null)
            {
                return BadRequest("Pedido inválido");
            }

            var order = new Order
            {
                client_id = createOrder.client_id,
                seller_id = createOrder.seller_id,
                delivery_type = createOrder.delivery_type,
                order_status = createOrder.order_status,
                total_price = createOrder.total_price,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        public async Task<ActionResult<UpdateOrderDTO>> UpdateOrder(int id, [FromBody]UpdateOrderDTO updateOrder)
        {
            if (updateOrder == null)
            {
                return BadRequest("Pedido inválido");
            }

            var order = await _context.Orders.FirstOrDefaultAsync(c => c.id == id);

            if (order == null)
            {
                return NotFound("Pedido não encontrado");
            }

            order.order_status = updateOrder.order_status;

            await _context.SaveChangesAsync();

            return Ok(order);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(c => c.id == id);

            if (order == null)
            {
                return NotFound("Pedido não encontrado");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok("Pedido cancelado.");
        }
    }
}