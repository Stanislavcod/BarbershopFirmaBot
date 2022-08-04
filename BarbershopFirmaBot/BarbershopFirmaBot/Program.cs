using AutoMapper;
using BarbarBot.Common.Mapper;
using BarberBot.BusinessLogic.Interfaces;
using BarberBot.BusinessLogic.Services;
using BarberBot.Model.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

var builder = new ConfigurationBuilder();
BuildConfig(builder);
builder.SetBasePath(Directory.GetCurrentDirectory());
builder.AddJsonFile("appsettings.json");
var config = builder.Build();
string connection = config.GetConnectionString("DefaultConnection");

var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
IMapper mapper = mappingConfig.CreateMapper();

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<ICityService, CityService>();
        services.AddTransient<IEmployeeService, EmployeeService>();
        services.AddTransient<IAmenitiesService, AmenitiesService>();
        services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
        services.AddSingleton(mapper);
    })
    .UseSerilog()
    .Build();
var _cityService = ActivatorUtilities.CreateInstance<CityService>(host.Services);
var _amenitiesService = ActivatorUtilities.CreateInstance<AmenitiesService>(host.Services);
var _employeeService = ActivatorUtilities.CreateInstance<EmployeeService>(host.Services);

static void BuildConfig(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsetings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT") ?? "Production"}.json", optional: true)
        .AddEnvironmentVariables();
}

#region Bot
var botClient = new TelegramBotClient("5406716149:AAF-2VzgKzw05pAfbLdDoNJDVS3qIXF8x2w");
using var cts = new CancellationTokenSource();
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { }
};
botClient.StartReceiving(HandleUpdates, HandleError, receiverOptions, cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();
Console.WriteLine($"Имя: {me.Username}");
Console.ReadLine();


cts.Cancel();

async Task HandleUpdates(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Type == UpdateType.Message && update?.Message?.Text != null)
    {
        await HandleMessage(botClient, update.Message);
        return;
    }
    if (update.Type == UpdateType.CallbackQuery)
    {
        await HandleCallbackQuery(botClient, update.CallbackQuery);
        return;
    }
}
async Task HandleMessage(ITelegramBotClient botClient, Message message)
{
    if (message.Text == "/start")
    {
        ReplyKeyboardMarkup keyboard = new(new[]
        {
            new KeyboardButton[] { "Записаться"}
        })
        {
            ResizeKeyboard = true
        };
        await botClient.SendTextMessageAsync(message.Chat.Id, "Выбрать:", replyMarkup: keyboard);
        return;
    }
    if (message.Text == "Записаться")
    {
        foreach (var item in _cityService.Get())
        {
            InlineKeyboardMarkup keyboard = new(new[]
        {
            new[]
            {
            InlineKeyboardButton.WithCallbackData(item.Name, item.Name),
            }
        });
            await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите город:", replyMarkup: keyboard);
        }
        return;
    }
}
async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    if (_cityService.Get().Any(x => callbackQuery.Data.StartsWith(x.Name)))
    {
        List<InlineKeyboardButton> listButton = new List<InlineKeyboardButton>();
        foreach (var item in _employeeService.Get())
        {
            listButton.Add(InlineKeyboardButton.WithCallbackData(item.Name, item.Specialization));
        }
        InlineKeyboardMarkup keyboard = new(listButton.ToArray());
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберите Барбера:", replyMarkup: keyboard);
    }
    if(_employeeService.Get().Any(x=> callbackQuery.Data.StartsWith(x.Name)))
    {
        List<InlineKeyboardButton> listButton = new List<InlineKeyboardButton>();
        foreach(var item in _amenitiesService.Get())
        {
            listButton.Add(InlineKeyboardButton.WithCallbackData(item.Title, item.Price.ToString()));
        }
        InlineKeyboardMarkup keyboard = new(listButton.ToArray());
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберите Услугу:", replyMarkup: keyboard);
    }
}
Task HandleError(ITelegramBotClient client, Exception exception, CancellationToken cancellation)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
        => $"Ошибка телеграм АПИ:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
        _ => exception.ToString()
    };
    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}
#endregion

