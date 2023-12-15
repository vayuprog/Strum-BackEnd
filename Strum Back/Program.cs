using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Strum_Back.Mapper;
using Strum.Core.Interfaces.Repositories;
using Strum.Infrastructure;
using Strum.Infrastructure.Repositories;
using Strum.Logic.Commands;
using Strum.Security;

var builder = WebApplication.CreateBuilder(args);

// Connect to the database
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql("Host=localhost;Database=Strum;Username=postgres;Password=admin228");
});
builder.Services.AddMediatR(cfg => {cfg.RegisterServicesFromAssembly(typeof(UserCreateRequest).Assembly);});
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<JwtTokenGenerator>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();


