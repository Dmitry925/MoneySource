using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Features.TransactionFeatures.Commands;
using MoneySource.Core.Application.Features.TransactionFeatures.Queries;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySource.Presentation.WebAPI.Controllers
{
    public class TransactionController : BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand.Request request)
        {
            return Ok(await SendAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions([FromQuery] GetAllTransactionsQuery.Request request)
        {
            return Ok(await SendAsync(request));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetTransactionByIdQuery.Request request)
        {
            return Ok(await SendAsync(request));
        }

        [HttpDelete("{Id:Guid}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] DeleteTransactionCommand.Request request)
        {
            return Ok(await SendAsync(request));
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateTransaction([FromRoute] Guid id, [FromBody] UpdateTransactionCommand.Request request)
        {
            request.Id = id;

            return Ok(await SendAsync(request));
        }
    }
}
