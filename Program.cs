
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Minio;
using Minio.AspNetCore;
using CBM_API.Ultilities;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace CBM_API;

public class Program
{
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMinio(option =>
        {
            option.Endpoint = $"{builder.Configuration["Minio:Host"]}";
            option.AccessKey = $"{builder.Configuration["Minio:AccessKey"]}";
            option.SecretKey = $"{builder.Configuration["Minio:SecretKey"]}";
            option.ConfigureClient(client =>
            {
                client.WithSSL(Convert.ToBoolean(builder.Configuration["Minio:WithSSL"]));
            });        
        });
        
        builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            optionsBuilder.UseSqlServer(builder.Configuration["ConnectionStrings:Production"]));

        builder.Services.AddControllersWithViews().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var PrivateKey = builder.Configuration["JWT:PrivateKey"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(PrivateKey));
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("https://localhost:3000",
                                        "http://localhost:14886",
                                        "http://localhost:14886",
                                        "https://localhost:5173",
                                        "https://cbm.npcetc.vn",
                                        "http://cbm.npcetc.vn",
                                        "*")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
        });
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        /*builder.Services.AddTransient<NotificationJob>();
        builder.Services.AddSingleton(provider =>
        {
            var scheduler = new StdSchedulerFactory().GetScheduler().Result;
            scheduler.JobFactory = new JobFactory(provider);
            return scheduler;
        });*/


        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors();
        app.MapControllers();


        var scheduler = app.Services.GetService<IScheduler>();
        /*var job = JobBuilder.Create<NotificationJob>()
            .WithIdentity("notificationJob")
            .Build();*/

        var trigger = TriggerBuilder.Create()
            .WithIdentity("notificationTrigger")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(5)
                .RepeatForever())
            .Build();

        /*scheduler.ScheduleJob(job, trigger);*/
        /*scheduler.Start();*/


        app.Run();
    }
}

