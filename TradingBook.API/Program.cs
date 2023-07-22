using TradingBook.Infraestructure;
using TradingBook.Application;

const string WEB_APP_CORS_POLICY = "WebApplicationPolicy";

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);

builder.Services.AddCors(opt => 
{
    opt.AddPolicy(WEB_APP_CORS_POLICY, config =>
    {
        config.AllowAnyHeader();
        config.AllowAnyMethod();
        config.AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(WEB_APP_CORS_POLICY);  

app.UseAuthorization();

app.MapControllers();

app.Run();
