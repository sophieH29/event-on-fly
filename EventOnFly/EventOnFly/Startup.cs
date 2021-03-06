using System;
using System.Net;
using System.Text;
using EventOnFly.DataAccess.Data;
using EventOnFly.DataAccess.Data.DbAccess;
using EventOnFly.DataAccess.Data.DbModels;
using EventOnFly.Web.Auth.Helpers;
using EventOnFly.Web.Extensions;
using EventOnFly.Modules.VendorModule.BusinessLogic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SpaServices.Webpack;
using EventOnFly.Web.Auth;
using EventOnFly.Web.Auth.Models;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Rewrite;

namespace EventOnFly
{
  public class Startup
  {
   
    private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // TODO: generate key and put in secret place, e.g. Azure wallet
    private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(options =>
      {
        options.Filters.Add(new Microsoft.AspNetCore.Mvc.RequireHttpsAttribute());
      });

      services.AddDbContext<EofDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));

      services.BuildServiceProvider().GetService<EofDbContext>().Database.Migrate();

      services.AddSingleton<IJwtFactory, JwtFactory>();

      // Register the ConfigurationBuilder instance of FacebookAuthSettings
      services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

      services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

      services.AddTransient<IRegistrationService, RegistrationService>();
      services.AddTransient<IProcedureExecutor, ProcedureExecutor>();
      services.AddTransient<IDbMediator, DbMediator>();
      services.AddTransient<IDbRequestWrapper, DbRequestWrapper>();

      // jwt wire up
      // Get options from app settings
      var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

      // Configure JwtIssuerOptions
      services.Configure<JwtIssuerOptions>(options =>
      {
        options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
        options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
      });

      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

        ValidateAudience = true,
        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = _signingKey,

        RequireExpirationTime = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
      };

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

      }).AddJwtBearer(configureOptions =>
      {
        configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        configureOptions.TokenValidationParameters = tokenValidationParameters;
        configureOptions.SaveToken = true;
      });

      // api user claim policy
      services.AddAuthorization(options =>
      {
        options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
        options.AddPolicy("VendorUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.VendorAccess));
      });

      // add identity
      var builder = services.AddIdentityCore<AppUser>(o =>
      {
              // configure identity options
              o.Password.RequireDigit = false;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 6;
      });
      builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
      builder.AddEntityFrameworkStores<EofDbContext>().AddDefaultTokenProviders();

      services.AddAutoMapper();
      services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
          HotModuleReplacement = true
        });
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseExceptionHandler(
        builder =>
        {
          builder.Run(
                    async context =>
                    {
                      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                      context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                      var error = context.Features.Get<IExceptionHandlerFeature>();
                      if (error != null)
                      {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                      }
                    });
        });

      app.UseAuthentication();
      app.UseDefaultFiles();
      app.UseStaticFiles();

      var options = new RewriteOptions().AddRedirectToHttps(StatusCodes.Status301MovedPermanently, 44355);
      app.UseRewriter(options);

      app.UseMvc(routes =>
            {
              routes.MapRoute(
                          name: "default",
                          template: "{controller=Home}/{action=Index}/{id?}");

              routes.MapSpaFallbackRoute(
                          name: "spa-fallback",
                          defaults: new { controller = "Home", action = "Index" });
            });

      
    }
  }
}
