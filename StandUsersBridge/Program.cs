using StandUsersBridge.Infrastructure.ServiceCollection;
using StandUsersBridge.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders("X-Pagination-Total-Pages")
           .WithExposedHeaders("X-Pagination-Next-Page")
           .WithExposedHeaders("X-Pagination-Has-Next-Page")
           .WithExposedHeaders("X-Pagination-Total-Pages");
});

app.UseMiddleware<MessageSendingExceptionHandler>();

app.UseAuthorization();

app.MapControllers();

app.Run();
