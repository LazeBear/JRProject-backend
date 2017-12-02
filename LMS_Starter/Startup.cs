using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS_Starter.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace LMS_Starter
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // pass value in appsettings.json to the ServiceConfiguration class
            services.Configure<ServiceConfiguration>(Configuration.GetSection("ServiceConfiguration"));

            // keep the capital letter in response json key
            services.AddMvc().AddJsonOptions((obj) => {
                if(obj.SerializerSettings != null) {
                    var castResolver = obj.SerializerSettings.ContractResolver as DefaultContractResolver;
                    castResolver.NamingStrategy = null;
                }
            });
            services.AddMvc();

            // add service
            services.AddTransient<ISMSService, SMSService>();
            services.AddTransient<IMailService, MailService>();

            // stop looping around between relations, like student try to get address and address looks for student
            services.AddMvc().AddJsonOptions(
                o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });




            // set up swagger
            var title = Configuration.GetSection("SwaggerConfig")["Title"];
            var versionName = Configuration.GetSection("SwaggerConfig")["Version"];
            services.AddSwaggerGen(c =>{
                c.SwaggerDoc("v1", new Info { Title = title, Version = versionName });
                var basePath = AppContext.BaseDirectory;
                var assemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                var fileName = System.IO.Path.GetFileName(assemblyName + ".xml");
                c.IncludeXmlComments(System.IO.Path.Combine(basePath, fileName));
            });

            services.AddDbContext<DBContext>();
            services.AddScoped<IDataStore, DataStore>();
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));

			loggerFactory.AddDebug();

			app.UseDeveloperExceptionPage();

            ///
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            ////////

			app.UseMvc();
		}
    }
}
