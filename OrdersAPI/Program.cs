using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Filters;
using OrdersAPI.Middlewares;
using OrdersAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // APPROACH 1: Custom Action Filter
    // options.Filters.Add<ValidateModelFilter>();
});

// APPROACH 2: Framework Built-in [ApiController] Automatic Validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{    
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var customProblem = new
        {
            Status = 400,
            Title = "Model Validation Failed",
            Errors = errors
        };

        return new BadRequestObjectResult(customProblem);
    };
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderRequestValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
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

app.UseCors("AllowAll");

app.UseAuthorization();

// using our custom middleware
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();
