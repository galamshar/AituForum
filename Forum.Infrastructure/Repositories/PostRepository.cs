using AutoMapper;

using Forum.Domain.Exceptions;
using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;
using Forum.Infrastructure.Data;
using Forum.Infrastructure.Data.Entities;
using Forum.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IMapper _mapper;
        private readonly ForumContext _db;

        public PostRepository(IMapper mapper, ForumContext db)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }


        public async Task<Post> FindById(long postId, CancellationToken cancellationToken = default)
        {
            var post = await _db.Posts
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == postId, cancellationToken);

            if (post is null)
            {
                throw new EntityNotFoundException(typeof(Post));
            }

            return _mapper.Map<Post>(post);
        }

        public async Task<Page<Post>> FindByTopicId(long topicId, Pagination pagination, CancellationToken cancellationToken = default)
        {
            var posts = await _db.Posts
                .AsNoTracking()
                .Where(p => p.TopicId == topicId).OrderBy(p => p.WrittenDate)
                .ToPageAsync(pagination, cancellationToken);

            return posts.Map(p => _mapper.Map<Post>(p));
        }

        public async Task<Page<Post>> FindByAuthorId(long authorId, Pagination pagination, CancellationToken cancellationToken = default)
        {
            var posts = await _db.Posts
                .AsNoTracking()
                .Where(post => post.AuthorId == authorId)
                .ToPageAsync(pagination, cancellationToken);

            return posts.Map(post => _mapper.Map<Post>(post));
        }

        public async Task Save(Post post, CancellationToken cancellationToken = default)
        {
            var dbPost = _mapper.Map<PostEntity>(post);

            _db.Posts.Add(dbPost);

            await _db.SaveChangesAsync(cancellationToken);

            post.Id = dbPost.Id;
        }

        public async Task Update(Post post, CancellationToken cancellationToken = default)
        {
            var dbPost = await _db.Posts
                .SingleOrDefaultAsync(p => p.Id == post.Id, cancellationToken);

            if (dbPost is null)
            {
                throw new EntityNotFoundException(typeof(Post));
            }

            dbPost.Text = post.Text;
            dbPost.UpdatedDate = post.UpdatedDate;

            await _db.SaveChangesAsync(cancellationToken);
        }

        public Task Delete(long postId, CancellationToken cancellationToken = default)
        {
            var postToDelete = new PostEntity { Id = postId };

            _db.Posts.Attach(postToDelete);
            _db.Posts.Remove(postToDelete);

            return _db.SaveChangesAsync(cancellationToken);
        }

        public Task<int> CountByAuthorId(long authorId, CancellationToken cancellationToken = default)
        {
            return _db.Posts.CountAsync(post => post.AuthorId == authorId, cancellationToken);
        }

        public Task<int> CountByTopicId(long topicId, CancellationToken cancellationToken = default)
        {
            return _db.Posts.CountAsync(post => post.TopicId == topicId, cancellationToken);
        }

        public async Task<Post> GetLastPostByTopicId(long topicId, CancellationToken cancellationToken = default)
        {
            var lastPost = await _db.Posts.Where(p => p.TopicId == topicId).OrderByDescending(p => p.UpdatedDate).FirstOrDefaultAsync(cancellationToken);
            return _mapper.Map<Post>(lastPost);
        }
    }
}
