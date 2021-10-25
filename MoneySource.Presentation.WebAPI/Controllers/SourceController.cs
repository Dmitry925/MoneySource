using Microsoft.AspNetCore.Mvc;
using MoneySource.Core.Application.Features.SourceFeatures.Commands;
using MoneySource.Core.Application.Features.SourceFeatures.Queries;
using System;
using System.Threading.Tasks;

namespace MoneySource.Presentation.WebAPI.Controllers
{
    public class SourceController : BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> CreateSource([FromBody] CreateSourceCommand.Request request)
        {
            return Ok(await SendAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSources([FromQuery] GetAllSourcesQuery.Request request)
        {
            return Ok(await SendAsync(request));
        }

        [HttpGet("{Id:Guid}")]
        public async Task<IActionResult> GetSourceById([FromRoute] GetSourceByIdQuery.Request request)
        {
            return Ok(await SendAsync(request));
        }

        [HttpDelete("{Id:Guid}")]
        public async Task<IActionResult> DeleteSource([FromRoute] DeleteSourceCommand.Request request)
        {
            return Ok(await SendAsync(request));
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateSource([FromRoute] Guid id, [FromBody] UpdateSourceCommand.Request request)
        {
            request.Id = id;

            return Ok(await SendAsync(request));
        }
    }
}
