using FuelAppAPI.Models.Database;
using FuelAppAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<FuelDatabaseSettings>(
    builder.Configuration.GetSection("FuelDatabaseSettings"));

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<NoticeService>();
builder.Services.AddSingleton<AuthService>();

builder.Services.AddSingleton<FeedbackService>();
builder.Services.AddSingleton<FavouriteService>();

builder.Services.AddSingleton<FuelStationService>();
builder.Services.AddSingleton<FuelStationArchiveService>();
builder.Services.AddSingleton<QueueLogService>();
builder.Services.AddSingleton<FuelStationLogService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();