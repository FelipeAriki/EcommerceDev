using EcommerceDev.Application;
using EcommerceDev.Core;
using EcommerceDev.Infrastructure;
using EcommerceDev.Infrastructure.BackgroundJobs;
using EcommerceDev.Infrastructure.SignalR;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplication()
    .AddCore()
    .AddInfrastructure(builder.Configuration);

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]
                    ?? throw new InvalidOperationException("JWT SecretKey not configured")))
        };
    });

builder.Services.AddControllers();

builder.Services.AddAuthorization();

// Add SignalR for real-time communication
builder.Services.AddSignalR();

// Add CORS to allow React frontend to connect
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Required for SignalR
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var recurringJobs = scope.ServiceProvider.GetService<IRecurringJobManager>();

    recurringJobs.AddOrUpdate<CancelExpiredOrdersJob>("expire-orders",
        job => job.ExecuteAsync(), Cron.Daily);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
    DashboardTitle = "EcommerceDev API Background Jobs",
});

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

// Map SignalR Hub endpoint
app.MapHub<PaymentNotificationHub>("/hubs/payment");

app.Run();
