using API_Client.Controllers;
using API_Client.Database;
using API_Client.Model.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

// todo change to new Syntax
var builder = WebApplication.CreateBuilder(args);

// load appsettings.json (idk if this is explicitly needed)
builder.Configuration.AddJsonFile("appsettings.json", false);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<BasicEndpointsController>();
builder.Services.AddSingleton<JwtTokenService>();
builder.Services.AddSingleton<UserController>();
builder.Services.AddScoped<UserDbContext>(); // needs to be scoped
builder.Services.AddScoped<UserDbService>();
// builder.Services.AddSingleton<DbEndpointsController>(); // gets automatically injected my ASP.net
builder.Services.AddScoped<UserDbService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() // allows all origins
        .AllowAnyMethod() // allows all methods
        .AllowAnyHeader(); // allows all headers
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Replace with your issuer
        ValidAudience = builder.Configuration["Jwt:Audience"], // Replace with your audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"])), // set an expected signature for the token
    };
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Your API",
            Version = "v1",
        });
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            Array.Empty<string>()
        },
    });
});

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<UserDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration["Db:ConnectionString"]);
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
