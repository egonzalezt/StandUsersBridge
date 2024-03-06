namespace StandUsersBridge.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Domain.Shared;
using Domain.User;
using Infrastructure.MessageBroker;
using Infrastructure.MessageBroker.Configuration;
using Infrastructure.MessageBroker.Publisher;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMessageSender _messageSender;
    private readonly StandUsersExchange _exchange;

    public UserController(IMessageSender messageSender, IOptions<StandUsersExchange> exchangeOptions)
    {
        _messageSender = messageSender;
        _exchange = exchangeOptions.Value;
    }

    [HttpPost()]
    public ActionResult Post([FromBody] UserDto user)
    {
        var headers = new Headers(EventTypes.CreateUser.ToString());
        _messageSender.SendMessage(user, _exchange.StandUsersRoutingKeys.Create,_exchange.Name, headers.GetAttributesAsDictionary());
        return Ok(user);
    }
}
