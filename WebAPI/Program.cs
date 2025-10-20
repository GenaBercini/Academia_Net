
using WebAPI;

var builder = WebApplication.CreateBuilder(args);
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();      
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
