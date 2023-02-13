using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SelfCare.Api.Config;
using SelfCare.Api.Requests.Misc.Joke;
using SelfCare.Application.Handlers.Misc.Joke;
using SelfCare.Application.Handlers.User.Get;
using SelfCare.Application.Handlers.User.Login;
using SelfCare.Application.Handlers.User.Signup;
using SelfCare.Application.Helpers;
using SelfCare.Clients.JokeAPI;
using SelfCare.Repository.MongoDB;
using System.Reflection;
using System.Text;

namespace SelfCare.Api
{
    public class Startup
    {
        private const string SwaggerPath = "/swagger";
        private const string AllowSpecificOriginsPolicyName = "AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IJokeClient, JokeClient>();

            services.AddScoped<IRequestHandler<JokeRequest, JokeResponse>, JokeHandler>();
            services.AddScoped<IRequestHandler<SignupRequest, SignupResponse>, SignupHandler>();
            services.AddScoped<IRequestHandler<LoginRequest, LoginResponse>, LoginHandler>();
            services.AddScoped<IRequestHandler<GetUserRequest, GetUserResponse>, GetUserHandler>();

            services.AddScoped<IMongoDbRepository, MongoDbRepository>();

            var corsOptions = new CorsConfig();
            Configuration.Bind(nameof(CorsConfig), corsOptions);

            services.Configure<JokeApiOptions>(options => Configuration.GetSection(nameof(JokeApiOptions)).Bind(options));
            services.Configure<MongoDbOptions>(options => Configuration.GetSection(nameof(MongoDbOptions)).Bind(options));

            services.AddCors(options =>
                options.AddPolicy(AllowSpecificOriginsPolicyName, corsPolicyBuilder =>
                {
                    corsPolicyBuilder.SetIsOriginAllowedToAllowWildcardSubdomains();
                    corsPolicyBuilder.WithOrigins(corsOptions.AllowedOrigins);
                    corsPolicyBuilder.AllowAnyHeader();
                    corsPolicyBuilder.AllowAnyMethod();
                    corsPolicyBuilder.AllowCredentials();
#if DEBUG
                    corsPolicyBuilder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
#endif
                }));

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddSwaggerDocument();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenService.TOKEN_KEY)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }); ;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerUi3(x => x.Path = SwaggerPath);
            app.UseOpenApi();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(AllowSpecificOriginsPolicyName);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
