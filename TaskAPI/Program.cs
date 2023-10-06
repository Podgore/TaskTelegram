using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Abstractions.Dialogues;
using TaskAPI.Abstractions.EF;
using TaskAPI.Abstractions.Services;
using TaskAPI.Application.Dialogues.Handlers;
using TaskAPI.Application.Telegram.Pooling;
using TaskAPI.BLL.Profiles;
using TaskAPI.BLL.Services;
using TaskAPI.DAL.EF;
using TaskAPI.DAL.Repositories;
using TaskAPI.Extensions;
using TaskAPI.Handlers.Task;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

var httpClient = new HttpClient();

builder.Services.AddAutoMapper(typeof(TaskProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddTaskCommandHandler).Assembly));

builder.Services.AddDbContext<Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepo<,>), typeof(RepoBase<,>));
builder.Services.RegisterDependenciesFromAssembly<IDialogueHandler, InitialDialogueHandler>(ServiceLifetime.Scoped);

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IPersonService, PersonService>();

var token = builder.Configuration.GetValue<string>("Telegram:Token") ?? throw new KeyNotFoundException("Unable to find Telegram:Token in appsettings.json");

builder.Services.AddHttpClient("telegram_bot_client")
 .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
 {
     TelegramBotClientOptions options = new(token);
     return new TelegramBotClient(options, httpClient);
 });

builder.Services.AddSingleton<UpdateHandler>();
builder.Services.AddScoped<ReceiverService>();
builder.Services.AddHostedService<PoolingService>();

builder.Services.AddControllers();

var app = builder.Build();

var mediator = app.Services.GetRequiredService<IMediator>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();