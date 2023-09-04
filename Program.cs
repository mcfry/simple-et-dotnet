using ExerciseTimer.Data;
using ExerciseTimer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        // "http://localhost:5173/", "localhost"
        builder.WithOrigins("http://localhost:5173") // Replace with your allowed origins
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Db Context
// builder.Services.AddSqlite<PizzaContext>("Data Source=ContosoPizza.db");
builder.Services.AddScoped<ExerciseService>();
builder.Services.AddScoped<SetRecordService>();
builder.Services.AddScoped<ExerciseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// enable logging
app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

// seed the db
app.CreateDbIfNotExists();

app.Run();
