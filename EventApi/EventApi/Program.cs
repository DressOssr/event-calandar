using EventApi.Services;
using EventApi.Utilis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using EventApi.Services.Interfaces;

namespace EventApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            const string AllowAllHeadersPolicy = "AllowAllHeadersPolicy";
            //builder.Services.AddCors();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name:AllowAllHeadersPolicy,
                    policy =>
                    {
<<<<<<< HEAD
                        policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
=======
                        //policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:9230").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                        policy.SetIsOriginAllowed(origin => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
>>>>>>> 682fefab4433ca557e060486ef92392605be8619
                    });

            });

            //builder.Services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.Lax;
            //});

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<CalendarEventDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CalendarEventDbConnectionString")));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = TokenHelper.Issuer,
                ValidAudience = TokenHelper.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper.Secret)),
                ClockSkew = TimeSpan.Zero
            };

        });

            builder.Services.AddAuthorization();

            builder.Services.AddTransient<IUserService,UserService>();
            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddTransient<IEventService, EventService>();
           // builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var app = builder.Build();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseCors(AllowAllHeadersPolicy);
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();    

            app.MapControllers();

            app.Run();
        }
    }
}