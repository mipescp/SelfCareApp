using MediatR;
using SelfCare.Api.Config;
using SelfCare.Api.Requests.Misc.Joke;
using SelfCare.Application.Handlers.Misc.Joke;
using SelfCare.Application.Handlers.User.Login;
using SelfCare.Application.Handlers.User.Signup;
using SelfCare.Clients.JokeAPI;
using SelfCare.Repository.MongoDB;
using System.Reflection;

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

            services.AddScoped<IMongoDbRepository, MongoDbRepository>();

            var corsOptions = new CorsConfig();
            Configuration.Bind(nameof(CorsConfig), corsOptions);

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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerUi3(x => x.Path = SwaggerPath);
            app.UseOpenApi();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(AllowSpecificOriginsPolicyName);
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        }
    }
}
