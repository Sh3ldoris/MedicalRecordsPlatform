using MedicalRecords.Data;
using MedicalRecords.Service;
using MedicalRecords.Service.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container 
builder.Services.AddControllers();
// Add DbContext with explicit generic type
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();


// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Medical Records API", 
        Version = "v1",
        Description = "API for managing medical records, doctors, and patients"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medical Records API V1");
    });
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();