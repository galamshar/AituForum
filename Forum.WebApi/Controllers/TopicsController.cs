using Forum.ApplicationLayer.Auth;
using Forum.ApplicationLayer.Posting;
using Forum.Domain.SeedWork;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TopicsController : Controller
    {
        private readonly IAuthContext _authContext;
        private readonly IMediator _mediator;

        public TopicsController(IAuthContext authContext, IMediator mediator)
        {
            _authContext = authContext ?? throw new ArgumentException(nameof(authContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("/count/{authorId}")]
        public async Task<IActionResult> CountTopics(long creatorId, CancellationToken cancellation)
        {
            return Ok(new
            {
                Count = await _mediator.Send(new CountTopicsByCreatorIdQuery(creatorId), cancellation)
            });
        }

        /// <summary>
        /// Gets root topics.
        /// </summary>
        /// <param name="pageNumber"> Number of page. </param>
        /// <param name="pageSize"> Size of page. </param>
        /// <param name="cancellationToken"> Cancellation token. </param>
        [HttpGet("root-topics")]
        public async Task<IActionResult> GetRootTopics(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetRootTopicsQuery(new Pagination(pageNumber, pageSize)), cancellationToken));
        }

        /// <summary>
        /// Gets sub topics of topic.
        /// </summary>
        /// <param name="topicId"> Id of parent topic. </param>
        /// <param name="pageNumber"> Number of page. </param>
        /// <param name="pageSize"> Size of page. </param>
        /// <param name="cancellationToken"> Cancellation token. </param>
        [HttpGet("sub-topics")]
        public async Task<IActionResult> GetSubTopics(
            long topicId,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new TopicCheckedQuery(topicId));
            return Ok(await _mediator.Send(new GetSubTopicsByParentIdQuery(topicId, new Pagination(pageNumber, pageSize)),
                cancellationToken));
        }

        /// <summary>
        /// Creates new root topic.
        /// </summary>
        /// <param name="creatorId"> Id of root topic creator </param>
        /// <param name="name"> Name of topic </param>
        /// <param name="canOwnPosts">  </param>
        /// <param name="rolesAllowedToRead"> Roles which allowed to read in topic </param>
        /// <param name="rolesAllowedToWrite"> Roles which allowed to write in topic </param>
        /// <param name="cancellationToken"> Cancelation token </param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("create-topic")]
        public async Task<IActionResult> CreateRootTopic(
            string name,
            string description,
            bool canOwnPosts,
            List<string> rolesAllowedToRead,
            List<string> rolesAllowedToWrite,
            CancellationToken cancellationToken
            )
        {
            var creatorId = (await _authContext.GetSignedAccount(cancellationToken)).Id;
            return Ok(await _mediator.Send(new CreateRootTopicCommand(creatorId, name, description, canOwnPosts, rolesAllowedToRead, rolesAllowedToWrite),
                cancellationToken));
        }

        /// <summary>
        /// Creates new subtopic
        /// </summary>
        /// <param name="creatorId"> Id of sub topic creator </param>
        /// <param name="parentTopicId"> Id of parent topic </param>
        /// <param name="name"> Name of subtopic </param>
        /// <param name="canOwnPosts"></param>
        /// <param name="rolesAllowedToRead"> Roles which allowed to read in topic </param>
        /// <param name="rolesAllowedToWrite"> Roles which allowed to write in topic </param>
        /// <param name="cancellationToken"> Cancelation token </param>
        /// <returns></returns>

        [HttpPost("create-subtopic")]
        public async Task<IActionResult> CreateSubTopic(
            long parentTopicId,
            string name,
            string description,
            bool canOwnPosts,
            List<string> rolesAllowedToRead,
            List<string> rolesAllowedToWrite,
            CancellationToken cancellationToken
            )
        {
            var creatorId = (await _authContext.GetSignedAccount(cancellationToken)).Id;
            return Ok(await _mediator.Send(new CreateSubTopicCommand(creatorId, parentTopicId, name, description, canOwnPosts, rolesAllowedToRead, rolesAllowedToWrite),
                    cancellationToken));
        }

        [HttpGet("posts-count/{topicId}")]
        public async Task<IActionResult> GetCountOfPostsByTopicId(
                long topicId,
                CancellationToken cancellationToken
            )
        {
            return Ok(await _mediator.Send(new CountPostsByTopicIdQuery(topicId), cancellationToken));
        }

        [HttpGet("fresh-post")]
        public async Task<IActionResult> GetFreshPostByTopicId(
            long topicId,
            CancellationToken cancellationToken)
        {
            var post = await _mediator.Send(new GetLastActivityByTopicIdQuery(topicId), cancellationToken);
            return Ok(post);
        }
    }
}
