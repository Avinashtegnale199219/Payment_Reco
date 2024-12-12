using System;
using FD_CP_BTP.App_Code.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using MvcOptionsExtensions;

namespace FD_PaymentReconciliation_V2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static IConfiguration Configuration { get; set; }

        //Added by 100002225
        private int SessionTimeout
        {
            get
            {
                string ConnectionTimeout = Convert.ToString(Configuration.GetSection("SessionTimeOut").Value);
                if (string.IsNullOrEmpty(ConnectionTimeout))
                {
                    ConnectionTimeout = "80";
                }
                return Convert.ToInt32(ConnectionTimeout);
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            GZipCompression(services);

            services.AddResponseCompression();

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //VAPT POINT HSTS Not Implemented          
            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new RequireHttpsAttribute());
            //});

            var sp = services.BuildServiceProvider();
            var logger = sp.GetService<ILoggerFactory>();
            var objectPoolProvider = sp.GetService<ObjectPoolProvider>();

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(31536000);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a timeout for session;
                options.IdleTimeout = TimeSpan.FromMinutes(this.SessionTimeout);
            });
            services.AddMvc(options => {
                options.Filters.Add(typeof(AuthorizationFilter));
                options.UseHtmlEncodeJsonInputFormatter(logger.CreateLogger<MvcOptions>(), objectPoolProvider);
                options.Filters.Add(typeof(ExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //VAPT POINT Click Jacking 
                app.Use(async (context, next) =>
                {
                    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                    await next();
                });
            }
            app.UseResponseCompression();


            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void GZipCompression(IServiceCollection services)
        {
            //Default Compression
            services.Configure<GzipCompressionProviderOptions>(options =>
            options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });
        }
    }
}
