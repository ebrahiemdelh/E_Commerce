using E_Commerce.Application.Contracts;
using E_Commerce.Infrastructure.Authentication;
using E_Commerce.Infrastructure.Identity.Entities;
using E_Commerce.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            AddJwtAuthentication(services, config);
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                var connection = config.GetConnectionString("IdentityConnection");
                ArgumentNullException.ThrowIfNullOrEmpty(connection);
                options.UseSqlServer(connection);
            });

            services.AddSingleton<IConnectionMultiplexer>(cfg =>
             {
                 return ConnectionMultiplexer.Connect(config.GetConnectionString("RedisConnection")!);
             });

            services.AddIdentityCore<ApplicationUser>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            //services.AddSingleton<IConnectionMultiplexer>(sp =>
            //{
            //    var configuration = ConfigurationOptions.Parse(config.GetConnectionString("RedisConnection"), true);
            //    return ConnectionMultiplexer.Connect(configuration);
            //});

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IDbInitializer, StoreIdentityDbInitializer>();
            //services.AddKeyedScoped<IDbInitializer, DbInitializer]("Service1");
            //services.AddKeyedScoped<IDbInitializer, DbInitializer]("Service2");

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserStore, UserStore>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            return services;
        }

        private static void AddJwtAuthentication(IServiceCollection services, IConfiguration config)
        {
            var jwtSection = config.GetSection(JwtSettings.SectionName);
            services.AddSingleton<IAccessTokenService, JwtAccessTokenGenerator>();
            services.Configure<JwtSettings>(jwtSection);
            var jwtSettings = jwtSection.Get<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,

                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),

                        RequireAudience = true,
                        RequireExpirationTime = true,
                        ValidateLifetime=true,

                        ClockSkew=TimeSpan.FromMinutes(1)
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            Console.WriteLine("========== JWT RECEIVED ==========");
                            Console.WriteLine(context.Token);
                            Console.WriteLine("==================================");

                            return Task.CompletedTask;
                        },

                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("========== JWT ERROR ==========");
                            Console.WriteLine(context.Exception.GetType().Name);
                            Console.WriteLine(context.Exception.Message);
                            Console.WriteLine("================================");

                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
