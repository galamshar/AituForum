using Forum.ApplicationLayer.Auth;
using Forum.Domain.AuthAggregate;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAuthContext _authContext;

        public AuthController(
            IMediator mediator,
            IAuthContext authContext)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(string login, string password, CancellationToken cancellationToken)
        {
            await _mediator.Send(new SignInCommand(login, password), cancellationToken);
            return new EmptyResult();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string login, string password, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RegisterAccountCommand(login, password, new Role[] { Role.Student }, true), cancellationToken);
            return new EmptyResult();
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> SignOut(CancellationToken cancellationToken)
        {
            await _authContext.SignOut(cancellationToken);
            return Ok();
        }
    }
}
