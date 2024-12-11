//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});

//// Add Swagger services
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddControllers();

//var app = builder.Build();

//// Enable Swagger only in development
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseRouting();
//app.UseCors("AllowAll");

//app.MapControllers();

//app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PerformanceTrackingService.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS (optional, if needed in your project).
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add Swagger (optional, for API documentation).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PerformanceContext>(options =>
    options.UseSqlServer(conString)); //put connection string in UseSqlServer

// Read the JWT secret key from appsettings.json and convert to bytes.
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]);

// Add JWT authentication.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Enable Swagger in development.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();



// Enable CORS and authentication middleware.
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
