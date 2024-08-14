using System;
using EcommerceAPI.Data;
using EcommerceAPI.DTOs;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            var clients = await _context.Clients.ToListAsync();

            if(clients == null)
            {
                return NotFound("Nenhum cliente encontrado");
            }

            return Ok(clients);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Client>> GetById(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.id == id);

            if(client == null)
            {
                return NotFound("Cliente não encontrado");
            }

            return Ok(client);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<Client>> CreateClient(CreateClientDTO createClient)
        {
            if(createClient == null)
            {
                return BadRequest("Cliente inválido");
            }

            var client = new Client
            {
                name = createClient.name,
                phone = createClient.phone,
                email = createClient.email,
                password = createClient.password,
                cpf_cnpj = createClient.cpf_cnpj,
                street = createClient.street,
                number = createClient.number,
                complement = createClient.complement,
                city = createClient.city,
                state = createClient.state,
                postal_code = createClient.postal_code,
                country = createClient.country
            };
         
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return Ok(client);
        }

        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        public async Task<ActionResult<Client>> UpdateClient(int id, [FromBody] UpdateClientDTO client_update)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.id == id);

            if(client == null)
            {
                return NotFound("Cliente não encontrado");
            }

            client.name = client_update.name ?? client.name;
            client.phone = client_update.phone ?? client.phone;
            client.email = client_update.email ?? client.email;
            client_update.id = client.id;

            await _context.SaveChangesAsync();

            return Ok(client_update);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.id == id);

            if(client == null)
            {
                return NotFound("Client não encontrado");
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Ok("Cliente removido com sucesso");
        }
    }
}