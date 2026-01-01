using Microsoft.EntityFrameworkCore;
using TodoApi.Models; 


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Enable OpenAPI/Swagger support 
builder.Services.AddOpenApi(); 

// Configure Entity Framework Core with an in-memory database
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Mapps the OpenAPI document (JSON endpoint)
    app.MapOpenApi();
    
    // Configures the interactive Swagger UI
    app.UseSwaggerUi(options =>
    {
        // This is where the UI gets the data (the JSON document)
        options.DocumentPath = "/openapi/v1.json"; 
        
    });

    // Redirects the root path ("/") to the Swagger UI in development mode ***
    app.MapGet("/", context =>
    {
        // Redirects to the default Swagger UI path, which is typically "/swagger"
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();