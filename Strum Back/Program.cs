using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Strum_Back;
using Strum_Back.Hubs;
using Strum_Back.Mapper;
using Strum.Core.Interfaces.Repositories;
using Strum.Infrastructure;
using Strum.Infrastructure.Repositories;
using Strum.Logic.Commands;
using Strum.Security;
using Strum_Back.Hubs;
using Strum_Back.Services;

var builder = WebApplication.CreateBuilder(args);

// Connect to the database
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql("Host=localhost;Database=Strum;Username=postgres;Password=admin228");
});

// Configure CORS to allow requests from your front-end application's origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddMediatR(cfg => {cfg.RegisterServicesFromAssembly(typeof(UserCreateRequest).Assembly);});
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddSignalR();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddScoped<UserService>();
builder.Services.AddTransient<EmailService>(provider => new EmailService("smtp.elasticemail.com", 2525, "ivanyushchuk05@gmail.com", "6FDA3DD798988EEC096028AF9C2076DAA917"));
builder.Services.AddTransient<TwoFAService>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Define the BearerAuth scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
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
            new string[] {}
        }
    });
});

builder.Services.AddJwt();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chatHub");

app.Run();


