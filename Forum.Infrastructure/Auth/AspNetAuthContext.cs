using Forum.ApplicationLayer.Auth;
using Forum.Domain.AuthAggregate;
using Forum.Domain.SeedWork;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Auth
{
    // TODO: Implement auth mechanism.

    public class AspNetAuthContext : IAuthContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AuthOptions _authOptions;

        public AspNetAuthContext(
            IHttpContextAccessor httpContextAccessor,
            IOptions<AuthOptions> authOptions,
            IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _authOptions = authOptions.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<Account> GetSignedAccount(CancellationToken cancellationToken = default)
        {
                string login = _httpContextAccessor.HttpContext.User.Identity.Name;

            return await _unitOfWork
                .GetRepository<IAccountRepository>()
                .FindByLogin(login);
        }

        public Task SignIn(Account account, CancellationToken cancellationToken = default)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Login)
            };

            foreach (var role in account.Roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name));
            }

            var identity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var jwt = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.Add(_authOptions.LifeTime),
                signingCredentials: new SigningCredentials(
                    _authOptions.SymmetricKey,
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return _httpContextAccessor.HttpContext.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    token = encodedJwt,
                    login = account.Login,
                    roles = account.Roles,
                    userId = account.Id
                }), cancellationToken);
        }

        public Task SignOut(CancellationToken cancellationToken = default)
        {
            // In JWT Auth system there is easy no way to manually expire token.
            return Task.CompletedTask;
        }

        public Task Update(Account account, CancellationToken cancellationToken = default)
        {
            // In JWT Auth system ther is no easy way to rewrite token.
            // So just give new auth token.
            return SignIn(account, cancellationToken);
        }
    }
}
