using Microsoft.AspNetCore.Mvc;
using MoneySource.Core.Application.Features.SourceFeatures.Commands;
using MoneySource.Core.Application.Features.SourceFeatures.Queries;
using System.Threading.Tasks;

namespace MoneySource.Presentation.WebAPI.Controllers
{
    public class SourceController : BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> CreateSource([FromBody] PostSourceCommand.Request request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSources([FromQuery] GetAllSourcesQuery.Request request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{Id:Guid}")]
        public async Task<IActionResult> GetSourceById([FromRoute] GetSourceByIdQuery.Request request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{Id:Guid}")]
        public async Task<IActionResult> DeleteSourceById([FromRoute] DeleteSourceByIdCommand.Request request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(Guid id, Source sourceUpdate)
        //{
        //    var source = _context.Sources.Where(a => a.Id == id).FirstOrDefault();
        //    if (source == null) return NotFound();
        //    else
        //    {
        //        source.Name = sourceUpdate.Name;
        //        source.CreationDate = sourceUpdate.CreationDate;
        //        await _context.SaveAsync();
        //        return Ok(source.Id);
        //    }
        //}
    }
}
