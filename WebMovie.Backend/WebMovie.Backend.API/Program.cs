using Misa.Amis.DL.BaseDL;
using WebMovie.Backend.BL.ActorBL;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.BL.CategoryBL;
using WebMovie.Backend.BL.CommentBL;
using WebMovie.Backend.BL.EpisodeBL;
using WebMovie.Backend.BL.MovieBL;
using WebMovie.Backend.BL.OrderUserBL;
using WebMovie.Backend.BL.RoleUserBL;
using WebMovie.Backend.BL.TrailerBL;
using WebMovie.Backend.BL.UserBL;
using WebMovie.Backend.DL;
using WebMovie.Backend.DL.ActorDL;
using WebMovie.Backend.DL.BaseDL;
using WebMovie.Backend.DL.CategoryDL;
using WebMovie.Backend.DL.CommentDL;
using WebMovie.Backend.DL.EpisodeDL;
using WebMovie.Backend.DL.MovieDL;
using WebMovie.Backend.DL.OrderUserDL;
using WebMovie.Backend.DL.RoleUserDL;
using WebMovie.Backend.DL.TrailerDL;
using WebMovie.Backend.DL.UserDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IBasaBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));

builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserDL, UserDL>();

builder.Services.AddScoped<IMovieBL, MovieBL>();
builder.Services.AddScoped<IMovieDL, MovieDL>();

builder.Services.AddScoped<ICategoryDL, CategoryDL>();
builder.Services.AddScoped<ICategoryBL, CategoryBL>();

builder.Services.AddScoped<IEpisodeDL, EpisodeDL>();
builder.Services.AddScoped<IEpisodeBL, EpisodeBL>();

builder.Services.AddScoped<IOrderUserDL, OrderUserDL>();
builder.Services.AddScoped<IOrderUserBL, OrderUserBL>();

builder.Services.AddScoped<IActorDL, ActorDL>();
builder.Services.AddScoped<IActorBL, ActorBL>();

builder.Services.AddScoped<ICommentDL, CommentDL>();
builder.Services.AddScoped<ICommentBL, CommentBL>();

builder.Services.AddScoped<IRoleUserDL, RoleUserDL>();
builder.Services.AddScoped<IRoleUserBL, RoleUserBL>();

builder.Services.AddScoped<ITrailerDL, TrailerDL>();
builder.Services.AddScoped<ITrailerBL, TrailerBL>();

DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
