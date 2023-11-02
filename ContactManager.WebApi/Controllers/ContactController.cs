using Microsoft.AspNetCore.Mvc;
using MediatR;
using ContactManager.Application.Contact.Commands.CreateContact;
using ContactManager.Application.Contact.Commands.DeleteContact;
using ContactManager.Application.Contact.Commands.UpdateContact;
using ContactManager.Application.Contact.Queries.GetById;
using ContactManager.Application.Contact.Queries.GetContacts;
using ContactManager.Domain.Contracts.Contact.Create;
using ContactManager.Domain.Contracts.Contact.GetAll;
using ContactManager.Domain.Contracts.Contact.GetById;
using ContactManager.Domain.Contracts.Contact.Update;
using MapsterMapper;

namespace ContactManager.WebApi.Controllers;

[ApiController]
[Route("contact")]
public class ContactController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ContactController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateContact(CreateContactRequest request){

        var command = _mapper.Map<CreateContactCommand>(request);

        var createContactResponse = await _mediator.Send(command);

        return createContactResponse.Match(
            result => Ok(new CreateContactResponse(result)),
            errors => Problem(errors)
            );

    }


    [HttpGet("/contacts")]
    public async Task<IActionResult> GetContacts([FromQuery] GetContactsRequest request){
        
        var query = _mapper.Map<GetContactsQuery>(request);
      
        var getContactsResult = await _mediator.Send(query);

        return getContactsResult.Match(
            result => Ok(new GetContactsResponse(result)),
            errors => Problem(errors)
            );
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetContactById([FromRoute] int id){
        
        var query = new GetContactQuery()
        {
            Id = id
        };
      
        var getContactResult = await _mediator.Send(query);

        return getContactResult.Match(
            result => Ok(new GetContactResponse(result)),
            errors => Problem(errors)
        );
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteContact([FromRoute] int id){
        
        var query = new DeleteContactCommand()
        {
            Id = id
        };
      
        var getContactResult = await _mediator.Send(query);

        return getContactResult.Match(
            result => Ok(result),
            errors => Problem(errors)
        );
    }
    
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateContact([FromRoute] int id, [FromBody] UpdateContactRequest request){
        
        var command = _mapper.Map<UpdateContactCommand>(request);
        command.Id = id;
      
        var getContactResult = await _mediator.Send(command);

        return getContactResult.Match(
            result => Ok(new UpdateContactResponse(result)),
            errors => Problem(errors)
        );
    }
}
