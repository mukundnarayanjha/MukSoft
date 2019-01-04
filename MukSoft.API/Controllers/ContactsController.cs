using MediatR;
using Microsoft.AspNetCore.Mvc;
using MukSoft.Core.Domain;
using MukSoft.Services.Contact.Command;
using MukSoft.Services.Contact.Query;
using System;
using System.Threading.Tasks;

namespace MukSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region "Get Contact"       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new ReadQuery());
            return result != null ? (IActionResult)Ok(result) : StatusCode(500);
        }

        [HttpGet("{id}", Name = "GetContacts")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            var result = await _mediator.Send(new ReadByIdQuery() { Id = id });
            return result != null ? (ActionResult)Ok(result) : NotFound();
        }
        #endregion

        #region "Insert/Update"        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Contact model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest(ModelState);
            var id = await _mediator.Send(new InsertCommand(model));
            return string.IsNullOrEmpty(id.ToString()) ? NotFound() : (IActionResult)Created(string.Empty, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Contact model)
        {
            if (model == null || !ModelState.IsValid) return BadRequest(ModelState);
            var result = await _mediator.Send(new UpdateCommand(id, model));
            return result ? (IActionResult)Created(string.Empty, id) : Ok(result);
        }
        #endregion

        #region "Delete"

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid? id)
        {
            if (id == Guid.Empty || id == null) return BadRequest(ModelState);
            var result = await _mediator.Send(new DeleteCommand(id));
            return result ? (IActionResult)Ok(result) : NotFound();
        }
        #endregion
    }
}