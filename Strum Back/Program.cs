using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddTransient<EmailService>(provider => new EmailService("smtp-relay.brevo.com", 587, "ivanyushchuk05@gmail.com", "Pf86J1tROpFKYIma"));
builder.Services.AddTransient<TwoFAService>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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


