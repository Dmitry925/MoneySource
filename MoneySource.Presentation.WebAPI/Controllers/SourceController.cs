using Microsoft.AspNetCore.Mvc;
using MoneySource.Core.Application.Features.SourceFeatures.Commands;
using MoneySource.Core.Application.Features.SourceFeatures.Queries;
using System.Threading.Tasks;

namespace MoneySource.Presentation.WebAPI.Controllers
{
    public class SourceController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllSources([FromQuery] GetAllSourcesQuery.Request request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSource([FromBody] PostSourceCommand.Request request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(Guid id)
        //{
        //    var source = await _context.Sources.Where(a => a.Id == id).FirstOrDefaultAsync();
        //    if (source == null) return NotFound();
        //    return Ok(source);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var source = await _context.Sources.Where(a => a.Id == id).FirstOrDefaultAsync();
        //    if (source == null) return NotFound();
        //    _context.Sources.Remove(source);
        //    await _context.SaveAsync();
        //    return Ok(source.Id);
        //}

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
