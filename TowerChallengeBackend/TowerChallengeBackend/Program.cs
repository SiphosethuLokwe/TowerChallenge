using TowerChallengeBackend.Interfaces;
using TowerChallengeBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register services 
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();

builder.Services.AddTransient<ILevelsService, LevelsService>();
builder.Services.AddTransient<IBoxService, BoxService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5173",
            builder => builder.WithOrigins("http://localhost:5173")
                              .AllowAnyMethod()
                              .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("AllowLocalhost5173"); // Apply the CORS polic

//app.UseAuthorization();
app.MapControllers();

app.Run();
