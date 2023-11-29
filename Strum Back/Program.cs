using Microsoft.EntityFrameworkCore;
using Strum.Core.Entities;
using Strum.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql("Host=localhost;Database=Strum;Username=postgres;Password=admin228");
});
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
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
//
//     // Run migrations
//     dbContext.Database.Migrate();
//
//     // Create an instance of DataSeeder
//     var dataSeeder = new DataSeeder();
//
//     // Call the FillUserTable method
//     dataSeeder.FillUserTable();
//
//     // Call the existing SeedData method if necessary
//     // dbContext.SeedData();
// }
app.MapControllers();

app.Run();


