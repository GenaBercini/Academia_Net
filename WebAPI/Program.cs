
using Data;
using Microsoft.EntityFrameworkCore;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(o => { });

// Add JWT Authentication
//var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//var secretKey = jwtSettings["SecretKey"];
//var issuer = jwtSettings["Issuer"];
//var audience = jwtSettings["Audience"];

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidIssuer = issuer,
//            ValidateAudience = true,
//            ValidAudience = audience,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
//            ClockSkew = TimeSpan.Zero
//        };
//    });

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = options.DefaultPolicy;
//});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var applyOnStart = config.GetValue<bool>("Migrations:ApplyOnStartup", app.Environment.IsDevelopment());
    var seedOnStart = config.GetValue<bool>("Migrations:SeedOnStartup", app.Environment.IsDevelopment());

    var db = scope.ServiceProvider.GetRequiredService<TPIContext>();

    //if (applyOnStart)
    //{
    //    db.Database.Migrate();
    //}

    //if (seedOnStart)
    //{
    //    DbSeeder.Initialize(db);
    //}
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(conection =>
    {
        conection.RoutePrefix = "swagger";
    });      
    app.UseHttpLogging();
}

app.UseHttpsRedirection();

// Use CORS
//app.UseCors("AllowBlazorWasm");

// Use Authentication & Authorization
//app.UseAuthentication();
//app.UseAuthorization();

//Map endpoints
app.MapAuthEndpoints();
app.MapUserEndpoints();
app.MapSubjectEndpoints();
app.MapPlanEndpoints();
app.MapCourseEndpoints();
app.MapSpecialtyEndpoints();


app.Run();
