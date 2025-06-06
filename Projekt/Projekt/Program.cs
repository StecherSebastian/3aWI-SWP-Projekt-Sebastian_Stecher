using Microsoft.EntityFrameworkCore;
using Projekt.Database;
using Projekt.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProjektDbContext>();

builder.Services.AddScoped<SchoolServices>();
builder.Services.AddScoped<SchoolRelationsServices>();
builder.Services.AddScoped<ClassroomServices>();
builder.Services.AddScoped<ClassroomRelationsServices>();
builder.Services.AddScoped<StudentServices>();
builder.Services.AddScoped<AggregationServices>();
builder.Services.AddScoped<QueryServices>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProjektDbContext>();
    dbContext.Database.Migrate();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseStaticFiles();
}

app.UseCors("AllowLocalhost");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();