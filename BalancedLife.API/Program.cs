using BalancedLife.Infra.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfrastructureJWT(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler("/error");
//Error Treatment
app.Map("/error", (HttpContext http) => {
    return Results.Problem(
        title: "Internal Server Error",
        statusCode: 500,
        detail: "An unexpected error occurred. Please try again later.",
        instance: http.Request.Path
    );
});

app.Run();
