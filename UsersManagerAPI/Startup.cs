using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using UsersManagerAPI.DataAccess;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;
using UsersManagerAPI.IServices;
using UsersManagerAPI.Services;
using UsersManagerAPI.Services.IServices;

namespace UsersManagerAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Global Variables
            Global.ConnectionString = Configuration.GetConnectionString("UserDB");
            Global.DomainName = Configuration["DomainName"];
            Global.JWTKey = Configuration["JWTKey"];
            #endregion

            #region DI Section
            services.AddDbContextPool<UsersBDContext>(option => option.UseSqlServer(Configuration.GetConnectionString("UserDB")));
            services.AddTransient<ITokensGenerator, TokensGenerator>();
            services.AddTransient<IAuthenticateUser, AuthenticateUser>();
            services.AddTransient<IUsersCRUD , UsersCRUD>();
            services.AddTransient<IRegisterUser, RegisterUser>();
            services.AddTransient<IUserInfo, UserInfo>();
            services.AddTransient<IConfirmMail, ConfirmMail>();
            services.AddTransient<IMailService, MailService>();
            #endregion

            services.AddControllers();

            //#region Authentication & Autherization
            //services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Global.JWTKey)),
            //        ValidateLifetime = true,
            //        ValidateAudience = false,
            //        ValidateIssuer = false,
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});
            //#endregion
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
