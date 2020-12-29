using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramTestBot.Services;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace TelegramTestBot
{
    public class Startup
    {
        private readonly IConfiguration _config;
        //private readonly BotService _botService;

        //public IConfiguration Configuration { get; }
        //public IConfiguration Configuration { get; private set; }

        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            //_env = env;
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(_env.ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddEnvironmentVariables();

            //_config = builder.Build();
            //var bot = new Telegram.Bot.TelegramBotClient(_config.GetSection("TelegramSettings:bot_token").Value);
            //bot.SetWebhookAsync("https://localhost:44322/Update").Wait();
            //bot.SetWebhookAsync("https://localhost:44322/api/webhook").Wait();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)//, BotService botService)
        {
            //services.AddScoped<IUpdateService, UpdateService>();
            //services.AddSingleton<IBotService, BotService>();
            services.AddSingleton<IConfiguration>(_config);
            //services.Configure<BotConfiguration>(_config.GetSection("BotConfiguration"));
            //services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}