var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Minimal APIs
app.MapGet("/time/utc", () => Results.Ok(DateTime.UtcNow));
app.MapGet("/", () => Results.Ok("Ok : SuccessHello Nguyen Nhan!"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
