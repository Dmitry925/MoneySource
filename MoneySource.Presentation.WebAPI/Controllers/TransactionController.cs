using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySource.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private IApplicationDbContext _context;

        public TransactionController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveAsync();
            return Ok(transaction.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transaction = await _context.Transactions.ToListAsync();
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var transaction = await _context.Transactions.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var transaction = await _context.Transactions.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (transaction == null) return NotFound();
            _context.Transactions.Remove(transaction);
            await _context.SaveAsync();
            return Ok(transaction.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Transaction transactionUpdate)
        {
            var transaction = _context.Transactions.Where(a => a.Id == id).FirstOrDefault();
            if (transaction == null) return NotFound();
            else
            {
                transaction.Name = transactionUpdate.Name;
                transaction.CreationDate = transactionUpdate.CreationDate;
                transaction.Type = transactionUpdate.Type;
                transaction.Amount = transactionUpdate.Amount;
                transaction.IsCompleted = transactionUpdate.IsCompleted;
                transaction.ComplitionDate = transactionUpdate.ComplitionDate;
                transaction.SourceId = transactionUpdate.SourceId;
                await _context.SaveAsync();
                return Ok(transaction.Id);
            }
        }
    }
}
