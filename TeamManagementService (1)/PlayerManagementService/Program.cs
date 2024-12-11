using Microsoft.EntityFrameworkCore;
using PlayerManagementService.Data; // Update the namespace to match your project structure

var builder = WebApplication.CreateBuilder(args);

// Configure services


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Optional: Add Swagger for API testing



var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PlayerContext>(options =>
    options.UseSqlServer(conString));


var app = builder.Build();

// Configure middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();

// Redirect root URL to a default page (optional)
app.MapGet("/", async context =>
{
    context.Response.Redirect("/index.html");
    await Task.CompletedTask;
});

app.MapControllers();

// Apply database migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PlayerContext>();
    db.Database.Migrate();
}

app.Run();
