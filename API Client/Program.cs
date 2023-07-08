using API_Client.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Dependency Injection
builder.Services.AddSingleton<BasicEndpointsController>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // "jwtbearer"
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // "jwtbearer"
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // for development
    options.SaveToken = true; //saves the token in HttpContext, accessible with 'await HttpContext.GetTokenAsync("access_token");'
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // check if the tokens Issuer matches
        ValidateAudience = true, // check if the tokens Audience matches
        ValidateLifetime = true, // check if the token is still valid and not expired
        ValidateIssuerSigningKey = true, // weather to validate the signature of the token
        ValidIssuer = "my_issuer", // Replace with your issuer
        ValidAudience = "my_audience", // Replace with your audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Keqing")) // set an expected signature for the token
    };
});

var app = builder.Build();

// use JWT-Token for authentication
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();