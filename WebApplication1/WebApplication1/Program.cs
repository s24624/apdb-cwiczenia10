using Microsoft.EntityFrameworkCore;
using WebApplication1.Repositories;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<HospitalDbContext>(opt =>
{
    opt.UseSqlServer("Data Source=localhost,1433; User ID=SA; Password=yourStrong(!)Password; Initial Catalog=apdb6; Integrated Security=False; Connect Timeout=30; Encrypt=False");
});
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();