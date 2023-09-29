using ProbabilityCalculator;
using ProbabilityCalculator.Functions;
using ProbabilityCalculator.Services;
using ProbabilityCalculator.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Scan(scan =>
    scan.FromCallingAssembly()
        .AddClasses(classes => classes.AssignableTo<IProbabilityFunction>())
        .AsImplementedInterfaces()
        .WithSingletonLifetime());

builder.Services.AddSingleton<IProbabilityCalculatorService, ProbabilityCalculatorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

Endpoints.Map(app);
app.Run();