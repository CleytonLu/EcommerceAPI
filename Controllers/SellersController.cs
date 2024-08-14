using System;
using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Models;
using EcommerceAPI.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SellersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SellerServices _services;

        public SellersController(AppDbContext context, SellerServices services)
        {
            _context = context;
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seller>>> GetAllSellers()
        {
            var sellers = await _context.Sellers.ToListAsync();

            if (sellers == null)
            {
                return NotFound("Vendedor não encontrado");
            }
            ;

            return Ok(sellers);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Seller>> GetByIdSeller(int id)
        {
            var seller = await _context.Sellers.FirstOrDefaultAsync(c => c.id == id);

            if (seller == null)
            {
                return NotFound("Vendedor não encontrado");
            }

            return Ok(seller);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<Seller>> CreateSeller(CreateSellerDTO createSeller)
        {
            if (createSeller == null)
            {
                return BadRequest("Vendedor inválido");
            }

            var reference = await _services.GetNextReference();

            var seller = new Seller
            {
                name = createSeller.name,
                email = createSeller.email,
                password = createSeller.password,
                phone = createSeller.phone,
                reference = reference
            };

            await _context.Sellers.AddAsync(seller);
            await _context.SaveChangesAsync();

            return Ok(seller);
        }

        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        public async Task<ActionResult<Seller>> UpdateSeller(
            int id,
            [FromBody] UpdateSellerDTO seller_update
        )
        {
            var existReference = await _context.Sellers.AnyAsync(s =>
                s.reference == seller_update.reference
            );

            if (existReference)
            {
                var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.id == id);
                if (seller == null)
                {
                    return BadRequest("Vendedor inválido");
                }

                seller.name = seller_update.name ?? "";
                seller.password = seller_update.password ?? "";
                seller.phone = seller_update.phone ?? "";
                seller.reference = seller_update.reference ?? "";

                _context.Sellers.Update(seller);
                await _context.SaveChangesAsync();

                return Ok(seller);
            }

            return NotFound("Não existe nenhum registro.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.id == id);

            if (seller == null)
            {
                return NotFound("Vendedor não encontrado");
            }

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();

            return Ok("Vendedor removido com sucesso");
        }
    }
}
