using Application.Common;
using Application.Danhsachlinhkien;
using Application.Dathang;
using Application.Lichsuthaotac;
using Application.Model;
using Application.System.User;
using Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Utilities.Constants;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebSparePartContext>(options =>
    options.UseSqlServer(builder.Configuration
    .GetConnectionString(SystemConstants.MainConnectionString)));

builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddTransient<IDanhsachlinhkienService, DanhsachlinhkienService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IModelService, ModelService>();
builder.Services.AddTransient<IDathangService, DathangService>();
builder.Services.AddTransient<ILichsuthaotacService, LichsuthaotacService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
