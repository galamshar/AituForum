using Forum.ApplicationLayer.Auth;
using Forum.Domain.AuthAggregate;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.WebApi.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Method that allows admins to manualy create accounts in systems.
        /// </summary>
        /// <param name="login"> Login of new account. </param>
        /// <param name="password"> Password of new account. </param>
        /// <param name="roles"> Roles of new account. </param>
        /// <param name="cancellationToken"> Cancellation token. </param>
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromForm(Name = "login")] string login,
            [FromForm(Name = "password")] string password, 
            [FromForm(Name = "roles")] List<string> roles,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new RegisterAccountCommand(
                login,
                password, 
                roles.Select(r => Role.FromName(r)),
                false),
                cancellationToken);

            return NoContent();
        }
    }
}
