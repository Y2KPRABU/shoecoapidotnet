using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Text.Json.Serialization; // Add this using directive for Swagger support

var builder = WebApplication.CreateBuilder(args);
/*builder.Services.AddOpenApi();

// Add Swagger services to the container
builder.Services.AddEndpointsApiExplorer();

using Microsoft.OpenApi.Models; // Ensure this using directive is present
using Swashbuckle.AspNetCore.SwaggerGen; // Add this using directive for Swagger support
using Swashbuckle.AspNetCore.SwaggerUI; // Add this using directive for 
*/
Debug.WriteLine($"Application Name: {builder.Environment.ApplicationName}");
Debug.WriteLine($"Environment Name: {builder.Environment.EnvironmentName}");
Debug.WriteLine($"ContentRoot Path: {builder.Environment.ContentRootPath}");
Debug.WriteLine($"WebRootPath: {builder.Environment.WebRootPath}");

var app = builder.Build();

app.UseHttpsRedirection();

var sampleTodos = new Todo[] {
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

