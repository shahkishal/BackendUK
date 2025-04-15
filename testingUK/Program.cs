using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using testingUK.Data;
using testingUK.MappingsS;
using testingUK.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular app URL
              .AllowAnyMethod()                     // Allow all HTTP methods (GET, POST, etc.)
              .AllowAnyHeader()                     // Allow any headers
              .AllowCredentials();                  // Allow cookies and credentials
    });
});


// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TripDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("TripConnectionString")));

builder.Services.AddDbContext<TripAuthDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("TripAuthConnectionString")));

builder.Services.AddDbContext<LikeDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("LikeAuthConnectionString")));

builder.Services.AddDbContext<CommentDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("CommentAuthConnectionString")));

//-------------------------------------Uday cu ----------------------------------------

//builder.Services.AddDbContext<TripDbContext>(options =>
//{

//    options.UseNpgsql(builder.Configuration.GetConnectionString("TripConnectionString1"));
//});


//builder.Services.AddDbContext<TripDbContext>(options =>
//{

//    options.UseNpgsql(builder.Configuration.GetConnectionString("TripAuthConnectionString1"));
//});


//builder.Services.AddDbContext<TripDbContext>(options =>
//{

//    options.UseNpgsql(builder.Configuration.GetConnectionString("LikeAuthConnectionString1"));
//});


//builder.Services.AddDbContext<TripDbContext>(options =>
//{

//    options.UseNpgsql(builder.Configuration.GetConnectionString("CommentAuthConnectionString1"));
//});

//-------------------------------------Uday cu ----------------------------------------

builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITravelTypeRepository,TravelTypeRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IBrowseRepository, BrowseRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Trip")
    .AddEntityFrameworkStores<TripAuthDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        }

    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
