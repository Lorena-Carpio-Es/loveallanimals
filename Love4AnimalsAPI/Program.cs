using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Love4AnimalsAPI.Data;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Services;
using Love4AnimalsAPI.Repositories;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName = builder.Configuration["Redis:InstanceName"];
});

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IDonationService, DonationService>();

builder.Services.AddScoped<ICacheService, CacheService>();
/*
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<CampaignRepository>();
builder.Services.AddSingleton<PostRepository>();
builder.Services.AddSingleton<CommentRepository>();
builder.Services.AddSingleton<DonationRepository>();
*/



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();