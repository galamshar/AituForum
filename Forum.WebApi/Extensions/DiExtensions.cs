using AutoMapper;
using Forum.ApplicationLayer.Accounting.Mapping;
using Forum.ApplicationLayer.Auth;
using Forum.ApplicationLayer.Data;
using Forum.ApplicationLayer.Posting.Mapping;
using Forum.Domain.SeedWork;
using Forum.Domain.Services;
using Forum.Infrastructure.Auth;
using Forum.Infrastructure.Data;
using Forum.Infrastructure.Data.Entities.Mapping;
using Forum.Infrastructure.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Forum.WebApi.Extensions
{
    public static class DiExtensions
    {
        public static IServiceCollection AddSimpleAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>();

            services.AddHttpContextAccessor();

            services.AddScoped<IAuthContext, AspNetAuthContext>();

            services.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = authOptions.SymmetricKey,
                    };
                });

            return services;
        }

        public static IServiceCollection AddEntityFrameworkDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ForumContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITransactionalUnitOfWork, TransactionalUnitOfWork>();

            return services;
        }

        public static IServiceCollection AddUtilityServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, NullPasswordHasher>();
            services.AddTransient<IDateTimeProvider, DefaultDateTimeProvider>();
            services.AddTransient<ISeedValuesExtractor, SeedValueExtractor>();
            return services;
        }

        public static IServiceCollection AddAutoMapperWithProfiles(this IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new EntityProfile());
                config.AddProfile(new PostProfile());
                config.AddProfile(new AccountProfile());
                config.AddProfile(new TopicProfile());
            });

            services.AddSingleton(mapperConfiguration.CreateMapper());

            return services;
        }
    }
}
