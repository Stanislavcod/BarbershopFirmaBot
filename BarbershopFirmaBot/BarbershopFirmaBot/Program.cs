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

#region configurationBuider
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
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IUserService, UserService>();
        services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
        services.AddSingleton(mapper);
    })
    .UseSerilog()
    .Build();
var _cityService = ActivatorUtilities.CreateInstance<CityService>(host.Services);
var _amenitiesService = ActivatorUtilities.CreateInstance<AmenitiesService>(host.Services);
var _employeeService = ActivatorUtilities.CreateInstance<EmployeeService>(host.Services);
var _userService = ActivatorUtilities.CreateInstance<UserService>(host.Services);
var _orderService = ActivatorUtilities.CreateInstance<OrderService>(host.Services);
#endregion

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
        await botClient.SendTextMessageAsync(message.Chat.Id, "Чего желаете?", replyMarkup: keyboard);
        return;
    }
    if (message.Text == "Записаться")
    {
        List<InlineKeyboardButton[]> listButton = new List<InlineKeyboardButton[]>();
        foreach (var item in _cityService.Get())
        {
            if(item != null)
            {
                listButton.Add(new[] { InlineKeyboardButton.WithCallbackData(text: item.Name, callbackData: item.Name) });
            }
        }
        InlineKeyboardMarkup keyboard = new(listButton.ToArray());
        await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите город:", replyMarkup: keyboard);
        return;
    }
}
async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    if (_cityService.Get().Any(x => callbackQuery.Data.StartsWith(x.Name)))
    {
        List<InlineKeyboardButton[]> listButton = new List<InlineKeyboardButton[]>();
        foreach (var item in _employeeService.Get(callbackQuery.Data))
        {
            if (item != null)
            {
                listButton.Add(new[] { InlineKeyboardButton.WithCallbackData(text: item.Name + ":" + item.Specialization, callbackData: item.Name) });
            }
        }
        InlineKeyboardMarkup keyboard = new(listButton.ToArray());
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберите Барбера:", replyMarkup: keyboard);
    }
    if(_employeeService.Get().Any(x=> callbackQuery.Data.StartsWith(x.Name)))
    {
        List<InlineKeyboardButton[]> listButton = new List<InlineKeyboardButton[]>();
        foreach(var item in _amenitiesService.Get())
        {
            listButton.Add(new[] { InlineKeyboardButton.WithCallbackData(text:item.Title +":" + item.Price + "BYN",callbackData:item.Price.ToString())});
        }
        InlineKeyboardMarkup keyboard = new(listButton.ToArray());
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберите Услугу:", replyMarkup: keyboard);
    }
    if(_amenitiesService.Get().Any(x=> callbackQuery.Data.StartsWith(x.Price.ToString())))
    {
        List<InlineKeyboardButton[]> listButton = new List<InlineKeyboardButton[]>();
        for (int i = DateTime.UtcNow.Day,j = DateTime.UtcNow.Month; i < DateTime.UtcNow.AddDays(7).Day; i++)
        {
            listButton.Add(new[] { InlineKeyboardButton.WithCallbackData(text: i.ToString()+"."+ j.ToString(), callbackData: i.ToString()) });
        }
        InlineKeyboardMarkup keyboard = new(listButton.ToArray());
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберите Дату:", replyMarkup: keyboard);
        //InlineKeyboardMarkup keyboard = new(new[]
        //{
        //    new []
        //    {
        //        InlineKeyboardButton.WithCallbackData(text:"1-5",callbackData:"1-5"),
        //        InlineKeyboardButton.WithCallbackData(text:"6-10",callbackData:"6-10"),
        //        InlineKeyboardButton.WithCallbackData(text:"11-15",callbackData:"11-15"),
        //    }, new []
        //    {
        //        InlineKeyboardButton.WithCallbackData(text:"16-20",callbackData:"16-20"),
        //        InlineKeyboardButton.WithCallbackData(text:"21-25",callbackData:"21-25"),
        //        InlineKeyboardButton.WithCallbackData(text:"26-31",callbackData:"26-31"),
        //    },
        //});
        //await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберите дату", replyMarkup: keyboard);
    }
    //if (callbackQuery.Data.StartsWith("1-5"))
    //{
    //    ChoiceDate(1, 5,botClient,callbackQuery);
    //}
    //if (callbackQuery.Data.StartsWith("6-10"))
    //{
    //    ChoiceDate(6, 10, botClient, callbackQuery);
    //}
    //if (callbackQuery.Data.StartsWith("11-15"))
    //{
    //    ChoiceDate(10, 16, botClient, callbackQuery);
    //}
    //if (callbackQuery.Data.StartsWith("16-20"))
    //{
    //    ChoiceDate(16, 20, botClient, callbackQuery);
    //}
    //if (callbackQuery.Data.StartsWith("21-25"))
    //{
    //    ChoiceDate(21, 25, botClient, callbackQuery);
    //}
    //if (callbackQuery.Data.StartsWith("26-31"))
    //{
    //    ChoiceDate(26, 31, botClient, callbackQuery);
    //}
}
//async Task ChoiceDate(int o, int b, ITelegramBotClient botClient, CallbackQuery callbackQuery)
//{
//    List<InlineKeyboardButton[]> listButton = new List<InlineKeyboardButton[]>();
//    for (int i = o; i <= b; i++)
//    {
//        listButton.Add(new[] { InlineKeyboardButton.WithCallbackData(text: i.ToString(), callbackData: i.ToString()) });
//    }
//    InlineKeyboardMarkup keyboard = new(listButton.ToArray());
//    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выберите число", replyMarkup: keyboard);
//}
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

