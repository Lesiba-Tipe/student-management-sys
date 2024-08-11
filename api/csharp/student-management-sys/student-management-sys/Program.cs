using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using student_management_sys.Configs;
using student_management_sys.Entity;
using student_management_sys.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure log4net logging
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net("log4net.config");

// Enable CORS
var corsPolicy = builder.Configuration.GetSection("CorsPolicy");
var clientPolicy = corsPolicy.GetSection("clientsCORSPolicy");
var clients = clientPolicy.Get<List<string>>()?.ToArray();

var clientsCORSPolicy = "MyAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(clientsCORSPolicy, policy =>
    {
        policy.WithOrigins(clients);
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<StudManSysDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<StudManSysDBContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
        options.TokenLifespan = TimeSpan.FromHours(2);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
    {
        var jwtOptions = builder.Configuration.GetSection("JwtOptions");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.GetSection("Issuer").Value, 
            ValidAudience = jwtOptions.GetSection("Audiance").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.GetSection("Key").Value)) 
        };
    }
);


// Configure authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("StudentPolicy", policy => policy.RequireRole("Student"));
    options.AddPolicy("ParentPolicy", policy => policy.RequireRole("Parent"));
});


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStudentService, StudentService>();

//Inject AutoMap
builder.Services.AddAutoMapper(typeof(MapperConfig));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "C# Student Management Sys API",
            Version = "v1",
            Description = "API System for Managing students"
        });

        //Define BearerAuth...
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJI\""
        }
        );

        option.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
                }
            },
            new string[] {}
        }});

    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(clientsCORSPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
