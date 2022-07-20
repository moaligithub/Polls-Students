using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using Polls.Core.Helpers;
using Polls.Core.Services;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.Interfaces.IUnitOfWork;
using Polls.EF.Data;
using Polls.EF.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Polls.Web
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
            services.AddControllersWithViews().AddNToastNotifyToastr(new NToastNotify.ToastrOptions
            {
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                TimeOut = 5000,
                CloseButton = true, 
                PreventDuplicates = true,  
            });

            services.AddDbContext<ApplicationDbContext>(option 
                => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), m 
                => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddAutoMapper(typeof(MappingProfile));

            // ADD UNIT OF WORK
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // ADD SERVICES
            services.AddTransient<IDepartmentServices, DepartmentServices>();
            services.AddTransient<ICourseServices, CourseServices>();
            services.AddTransient<ISessionServices, SessionServices>();
            services.AddTransient<IInstructorServices, InstructorServices>();
            services.AddTransient<IPollServices, PollServices>();
            services.AddTransient<IQuestionServices, QuestionServices>();
            services.AddTransient<IHomeServices, HomeServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/account/login";
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

            app.UseNToastNotify();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
