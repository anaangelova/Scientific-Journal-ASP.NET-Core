using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScientificJournal.Domain.Identity;
using ScientificJournal.Repository;
using ScientificJournal.Repository.Implementation;
using ScientificJournal.Repository.Interface;
using ScientificJournal.Service.Implementation;
using ScientificJournal.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificJournal.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());
            services.AddDefaultIdentity<ScienceUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IPaperRepository), typeof(PaperRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IPapersKeywordsRepository), typeof(PapersKeywordsRepository));
            services.AddScoped(typeof(IPapersUsersRepository), typeof(PapersUsersRepository));
            services.AddScoped(typeof(IPaperDocumentRepository), typeof(PaperDocumentRepository));
            services.AddScoped(typeof(IConferenceRepository), typeof(ConferenceRepository));


            services.AddTransient<IPaperService, PaperService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPaperDocumentService, PaperDocumentService>();
            services.AddTransient<IConferenceService, ConferenceService>();


            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Paper}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
          
        }
    }
}
