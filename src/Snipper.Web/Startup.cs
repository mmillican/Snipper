using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Snipper.Web.Services;
using Snipper.Web.Configuration;
using Amazon.DynamoDBv2;
using Snipper.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Snipper.Web.Models;
using Snipper.Web.Services.Implmentations.EntityFramework;

namespace Snipper.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dataProvider = Configuration["DataProvider"];

            services.AddControllersWithViews();

            if (Configuration.IsSqlServerDataProvider())
            {
                services.AddDbContext<SnipperDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")
                    )
                );

                services.AddTransient<ICategoryService, EfCategoryService>();
                services.AddTransient<ISnippetService, EfSnippetService>();
                services.AddTransient<ISearchService, EfSearchService>();
            }
            else // dynamo db + elasticsearch
            {
                services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
                services.AddAWSService<IAmazonDynamoDB>();

                services.Configure<DynamoConfig>(Configuration.GetSection("DynamoDb"));
                services.Configure<SearchConfig>(Configuration.GetSection("Search"));

                services.AddTransient<CategoryService>();
                services.AddTransient<SnippetService>();
                services.AddTransient<SnippetSearchService>();

                services.AddElasticSearch(Configuration);
            }

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.WithOrigins(
                        "http://localhost:8080"
                    );
                });
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Snipper API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // options.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Snipper API v1");
                config.RoutePrefix = "api-docs";
                config.DocumentTitle = "Snipper API";

                config.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                config.EnableDeepLinking();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8081");
                }
            });
        }
    }
}
