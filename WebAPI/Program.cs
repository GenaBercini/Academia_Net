using Application.Services;
using Data;
using Microsoft.EntityFrameworkCore;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<SubjectRepository>();
builder.Services.AddScoped<PlanRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<SpecialtyRepository>();
builder.Services.AddScoped<EnrollmentRepository>();

builder.Services.AddScoped<UserService>(); 
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<PlanService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<SpecialtyService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(o => { });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        policy =>
        {
            policy.WithOrigins("https://localhost:7154")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var applyOnStart = config.GetValue<bool>("Migrations:ApplyOnStartup", app.Environment.IsDevelopment());
    var seedOnStart = config.GetValue<bool>("Migrations:SeedOnStartup", app.Environment.IsDevelopment());

    var db = scope.ServiceProvider.GetRequiredService<TPIContext>();

    if (applyOnStart)
    {
        db.Database.Migrate();
    }

    if (seedOnStart)
    {
        DbSeeder.Initialize(db);
    }
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
app.UseCors("AllowBlazorWasm");

app.MapAuthEndpoints();
app.MapUserEndpoints();
app.MapSubjectEndpoints();
app.MapPlanEndpoints();
app.MapCourseEndpoints();
app.MapSpecialtyEndpoints();
app.MapEnrollmentEndpoints();

app.Run();
