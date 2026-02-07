var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Get the URL from the application
    var urls = app.Urls;
    var baseUrl = urls.FirstOrDefault() ?? "http://localhost:5112";
    Console.WriteLine($"\n✅ Application is running!");
    Console.WriteLine($"📚 Swagger UI: {baseUrl}/swagger/index.html\n");
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
