using Microsoft.EntityFrameworkCore;
using VoteLine.Domain;
using VoteLine.Persistence;
using VotLine.Infrastructure.Interfaces;
using VotLine.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//My Connection DB. -->
builder.Services.AddDbContext<VoteLineDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSQL"));
});

builder.Services.AddTransient<IUserRepository,UserRepository>();
builder.Services.AddTransient<ICandidateRepository,CandidateRepository>();
builder.Services.AddTransient<IVoteRepository, VoteRepository>();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "MyAllowSpecificOrigins",
//        builder =>
//        {
//            builder.WithOrigins("https://localhost",
//                                "https://localhost:7065")
//                   .AllowAnyMethod()
//                   .AllowAnyHeader()
//                   .SetIsOriginAllowedToAllowWildcardSubdomains();
//        });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost",
                                "https://localhost:7057",
                                "https://localhost:7065")
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

app.UseAuthorization();

app.MapControllers();

app.UseCors("MyAllowSpecificOrigins");

app.Run();

