
using CarBookingApp.Authentication;
using CarBookingApp.AuthenticationServices;
//using CarBookingApp.AuthenticationServices;
using CarBookingApp.Data;
using CarBookingApp.IServices;
using CarBookingApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
//using Tourism_Management_System_API.Authentication;
//using Tourism_Management_System_API.Services;
//using Tourism_Management_System_API_Project_.AuthenticationServices;
//using Tourism_Management_System_API_Project_.Data;
//using Tourism_Management_System_API_Project_.MappingProfiles;
using static CarBookingApp.Profiles.MappingProfiles;

namespace CarBookingApp { 
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            // Configure Entity Framework
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
                

            // Add Identity services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Dependency Injection
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddLogging();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IReviewManagementService, ReviewManagementService>();
            builder.Services.AddScoped<IInventoryService, InventoryService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ICarManagementService, CarManagementService>();
            builder.Services.AddScoped<IUserServiceManagement, UserServiceManagement>();
            builder.Services.AddScoped<AuthenticationServices.IAuthenticationService, AuthenticationServices.AuthenticationService>();
            //builder.Services.AddScoped<CarBookingApp.AuthenticationServices.IAuthenticationService, CarBookingApp.AuthenticationServices.AuthenticationService>();


         

            // Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                // Adding Jwt Bearer
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddLogging();

            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "please Enter Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Jwt",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                         {
                             new OpenApiSecurityScheme
                             {
                                    Reference = new OpenApiReference
                                    {
                                      Type = ReferenceType.SecurityScheme,
                                      Id = "Bearer"
                                     }
                             },
                             new string[] { }
                          }
                    });
            });
            builder.Services.AddLogging();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


