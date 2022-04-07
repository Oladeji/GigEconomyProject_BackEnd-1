using Microsoft.EntityFrameworkCore;
using IgpDAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;


using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});





builder.Services.AddDbContext<IgpDbContext>(option => { option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), x=> x.UseNetTopologySuite()); });
      // services.AddDbContext<AppDbContext>(options =>
        //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

// For Identity
builder.Services.AddIdentity<IgpUser, IdentityRole>()
    .AddEntityFrameworkStores<IgpDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew=TimeSpan.Zero,
        
        ValidIssuer= builder.Configuration["Jwt:JwtIssuer"],
        ValidAudience= builder.Configuration["Jwt:JwtAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Jwtkey"]))
    };
});


  // builder.Services.AddDefaultIdentity<IgpUser>(options => options.SignIn.RequireConfirmedAccount = true)
   // .AddEntityFrameworkStores<IgpDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});
    builder.Services.AddAuthorization();
    builder.Services.AddControllers();
    builder.Services.AddScoped<IJwtAuthManager,JwtAuthManager>();

    //builder.Services.AddSingleton<IJwtAuthManager>(new JwtAuthManager(builder.Configuration));
    builder.Services.AddScoped<IClientManager, Clientmanager>();
    builder.Services.AddSingleton<IJobManager,JobManager>();
  
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();



    var app = builder.Build();
    // Configure the HTTP request pipeline.
    app.UseAuthorization();
    app.UseAuthentication();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
    try
        {
           var context = services.GetRequiredService<IgpDbContext>();
           //context.Database.EnsureDeleted();//context.Database.EnsureCreated();//context.Database.Migrate();  
           IgpDbInitializer.Initialize(context);
        }
       catch (Exception ex)
      {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                    Console.WriteLine(ex.ToString());
       }
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

//dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.3
//dotnet add package Microsoft.IdentityModel.Tokens --version 6.16.0
//dotnet add package System.IdentityModel.Tokens.Jwt --version 6.16.0
//dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 6.0.3
//dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 6.0.3
//dotnet add package Microsoft.AspNetCore.Identity.UI --version 7.0.0-preview.2.22153.2
//I have add azure blob too


