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
using UsersManagerAPI.IServices;
using UsersManagerAPI.Services;

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
            Global.ConnectionString = Configuration.GetConnectionString("UserDB");
            Global.DomainName = Configuration["DomainName"];
            services.AddDbContextPool<UsersBDContext>(option => option.UseSqlServer(Configuration.GetConnectionString("UserDB")));
            services.AddTransient<ITokensGenerator, TokensGenerator>();
            services.AddTransient<IAuthenticateUser, AuthenticateUser>();
            services.AddTransient<IUsersCRUD , UsersCRUD>();
            services.AddControllers();
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("A345rbde&3yd(@%$Nckeoc-e9vjv97c9")),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
