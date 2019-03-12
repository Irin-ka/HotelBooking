using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HotelBooking.Extensions;
using HotelBooking.Models;
using HotelBooking.ModelBuilders.Abstract;
using HotelBooking.ModelBuilders.Concrete;
using HotelBooking.ControllerHelpers.RoomControllerHelpers.Abstract;
using HotelBooking.ControllerHelpers.RoomControllerHelpers.Concrete;
using HotelBooking.ControllerHelpers.UserControllerHelpers.Abstract;
using HotelBooking.ControllerHelpers.UserControllerHelpers.Concrete;

namespace HotelBooking {
    public class Startup {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<WdaContext>(options => options.UseMySql("Server=localhost;Database=wda_db;Uid=root;Pwd=wgp2kj4tTt"));

            services.AddIdentity<User, UserRole>().AddDefaultTokenProviders();
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<UserRole>, RoleStore>();

            //Services used to decouple controllers from model building and business logic
            services.AddTransient<IProfileModelBuilder, ProfileModelBuilder>();
            services.AddTransient<IFormBuilder, FormBuilder>();
            services.AddTransient<IRoomModelBuilder, RoomModelBuilder>();
            services.AddTransient<IRoomSearchBuilder, RoomSearchBuilder>();
            services.AddTransient<IFavoriteChanger, FavoriteChanger>();
            services.AddTransient<IBookingHandler, BookingHandler>();
            services.AddTransient<IReviewHandler, ReviewHandler>();
            services.AddTransient<IRegisterHandler, RegisterHandler>();

            services.Configure<IdentityOptions>(options => {
                //Password Settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;

                //User Settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options => {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute("searchresults", "{controller=Search}/{action=SearchResults}");
                routes.MapRoute("login", "{controller=User}/{action=Login}");
                routes.MapRoute("loginpage", "{controller=User}/{action=LoginPage}");
                routes.MapRoute("profile", "{controller=Home}/{action=Profile}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}");

            });

        }
    }
}
