using Forum.ApplicationLayer.Accounting;
using Forum.ApplicationLayer.Auth;
using Forum.ApplicationLayer.Posting;
using Forum.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAuthContext _authContext;

        public AccountsController(
            IMediator mediator,
            IAuthContext authContext)
        {
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetAccount(long accountId, CancellationToken cancellationToken)
        {
            //var accountId = (await _authContext.GetSignedAccount(cancellationToken)).Id;
            return Ok(await _mediator.Send(new GetAccountInfoQuery(accountId), cancellationToken));
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetAllPosts(
        long accountId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
        {
            //var authorId = (await _authContext.GetSignedAccount(cancellationToken)).Id;
            return Ok(await _mediator.Send(new GetPostsByAuthorIdQuery(
                accountId,
                new Pagination(pageNumber, pageSize)), cancellationToken));
        }

        [HttpGet("topics")]
        public async Task<IActionResult> GetAllTopics(
        long accountId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
        {
            //var authorId = (await _authContext.GetSignedAccount(cancellationToken)).Id;
            return Ok(await _mediator.Send(new GetTopicsByAuthorIdQuery(
                accountId,
                new Pagination(pageNumber, pageSize)), cancellationToken));
        }
    }
}
