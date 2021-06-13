using Forum.ApplicationLayer.Auth;
using Forum.ApplicationLayer.Posting;
using Forum.Domain.SeedWork;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAuthContext _authContext;

        public PostsController(
            IMediator mediator,
            IAuthContext authContext)
        {
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("/count/{authorId}")]
        public async Task<IActionResult> CountPosts(long authorId, CancellationToken cancellationToken)
        {
            return Ok(new
            {
                Count = await _mediator.Send(new CountPostsByAuthorIdQuery(authorId), cancellationToken)
            });
        }

        /// <summary>
        /// Gets post by its id.
        /// </summary>
        /// <param name="postId"> Id of post to read. </param>
        /// <param name="cancellationToken"> Cancellation token. </param>
        [HttpGet("{postId}")]
        public async Task<IActionResult> FindById(long postId, CancellationToken cancellationToken)
        {
            var readerId = (await _authContext.GetSignedAccount(cancellationToken)).Id;
            return Ok(await _mediator.Send(new GetPostByIdQuery(readerId, postId), cancellationToken));
        }

        [HttpGet("by-author/{authorId}")]
        public async Task<IActionResult> FindByAuthor(
            long authorId,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var readerId = (await _authContext.GetSignedAccount(cancellationToken)).Id;
            return Ok(await _mediator.Send(new GetPostsByAuthorIdQuery(
                authorId,
                readerId,
                new Pagination(pageNumber, pageSize)), cancellationToken));
        }



        /// <summary>
        /// Gets page of posts by topic id.
        /// </summary>
        /// <param name="topicId"> Id of topic. </param>
        /// <param name="pageNumber"> Number of page. </param>
        /// <param name="pageSize"> Size of page. </param>
        /// <param name="cancellationToken"> Cancellation token. </param>
        [HttpGet("by-topic/{topicId}")]
        public async Task<IActionResult> FindByTopicId(
            long topicId,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var readerId = (await _authContext.GetSignedAccount(cancellationToken)).Id;
            return Ok(await _mediator.Send(new GetPostsByTopicIdQuery(topicId, readerId, new Pagination(pageNumber, pageSize)), cancellationToken));
        }

        /// <summary>
        /// Posting new post in topic
        /// </summary>
        /// <param name="topicId"> Id of topic </param>
        /// <param name="authorId"> Id of author </param>
        /// <param name="text"> Text for posting </param>
        /// <param name="cancellationToken"> Cancelation token </param>
        /// <returns></returns>
        [HttpPost("{topicId}/post")]
        public async Task<IActionResult> CreatePost(
            long topicId,
            string text,
            CancellationToken cancellationToken
            )
        {
            var authorId = (await _authContext.GetSignedAccount(cancellationToken)).Id;
            await _mediator.Send(new CreatePostCommand(topicId, authorId, text));
            return Ok();

        }

        [HttpPost("edit-post")]
        public async Task<IActionResult> EditPost(long postId, string newText,CancellationToken cancellationToken)
        {
            await _mediator.Send(new EditPostCommand(postId, newText), cancellationToken);
            return Ok();
        }

        /*[HttpGet("allby-topic/{topicId}")]
        public async Task<IActionResult> FindAllByTopicId(
            long topicId,
            CancellationToken cancellationToken)
        {
            var readerId = (await _authContext.GetSignedAccount(cancellationToken)).Id;
            return Ok(_mediator.Send(new GetAllPostsByTopicIdQuery(topicId, readerId),cancellationToken));
        }*/
    }
}
