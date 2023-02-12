﻿namespace SelfCare.Api.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplication Build<TStartup>(this WebApplicationBuilder builder)
        {
            var startup = Activator.CreateInstance(typeof(TStartup), builder.Configuration);
            if (startup == null)
            {
                throw new InvalidOperationException("Could not instantiate Startup!");
            }

            var configureServices = typeof(TStartup).GetMethod("ConfigureServices");
            if (configureServices == null)
            {
                throw new InvalidOperationException("Could not find ConfigureServices on Startup!");
            }

            configureServices.Invoke(startup, new object[] { builder.Services });

            var app = builder.Build();

            var configure = typeof(TStartup).GetMethod("Configure");
            if (configure == null)
            {
                throw new InvalidOperationException("Could not find Configure on Startup!");
            }

            configure.Invoke(startup, new object[] { app, builder.Environment });

            return app;
        }
    }
}
