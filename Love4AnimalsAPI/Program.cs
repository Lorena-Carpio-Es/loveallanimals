using System.Text.Json.Serialization;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Services;
using Love4AnimalsAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositories (estado en memoria)
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<CampaignRepository>();
builder.Services.AddSingleton<PostRepository>();
builder.Services.AddSingleton<CommentRepository>();
builder.Services.AddSingleton<DonationRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IDonationService, DonationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(); ;