using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EcommerceAPI.services
{
    public class SellerServices(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        
        public async Task<string> GetNextReference()
        {
           var maxNumberSequence = await _context.Sellers
           .Where(s => s.reference.StartsWith("REF"))
           .Select(s => int.Parse(s.reference.Substring(3)))
           .DefaultIfEmpty(0)
           .MaxAsync();

            int nextNumberRef = maxNumberSequence + 1;
            var nextRef = $"REF{nextNumberRef:D4}";

            return nextRef;
        }

    }
}