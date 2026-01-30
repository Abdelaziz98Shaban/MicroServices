using Microsoft.EntityFrameworkCore;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Add DbContext with InMemory Database
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("inmem"));

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

// Add AutoMapper
builder.Services.AddAutoMapper(c => c.AddMaps(typeof(Program).Assembly));

var app = builder.Build();

// Seed the database
SeedData.PopulateData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Get the URL from the application
    var urls = app.Urls;
    var baseUrl = urls.FirstOrDefault() ?? "http://localhost:5003";
    Console.WriteLine($"\n✅ Application is running!");
    Console.WriteLine($"📚 Swagger UI: {baseUrl}/swagger/index.html\n");
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();


