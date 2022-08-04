using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BarberBot.Model.DataBaseContext;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

//var serviceProvider = new ServiceCollection()
//            .AddLogging()
//            .AddSingleton<IFooService, FooService>()
//            .AddSingleton<IBarService, BarService>()
//            .BuildServiceProvider();
var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {

                   services.AddLogging(configure => configure.AddConsole())
                   .AddDbContext<ApplicationContext>(options =>
                   {
                       options.UseSqlServer("Server=STASVCODE\\SQLEXPRESS;DataBase=BarbershopFirma;Trusted_Connection=True;TrustServerCertificate=True;");
                   });
               }).UseConsoleLifetime();

var host = builder.Build();


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
        await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите команду: /command");
    }
    if (message.Text == "/command")
    {
        ReplyKeyboardMarkup keyboard = new(new[]
        {
            new KeyboardButton[] { "Записаться", "Отменить заказ" }
        })
        {
            ResizeKeyboard = true
        };
        await botClient.SendTextMessageAsync(message.Chat.Id, "Выбрать:", replyMarkup: keyboard);
        return;
    }
    if (message.Text == "Заказать такси")
    {
        InlineKeyboardMarkup keyboard = new(new[]
        {
            new[]{
            InlineKeyboardButton.WithCallbackData("Эконом", "economy"),
            InlineKeyboardButton.WithCallbackData("Комфорт", "сomfort"),
            InlineKeyboardButton.WithCallbackData("Комфорт", "сomfort")
            },
             new[]{
            InlineKeyboardButton.WithCallbackData("Детский", "kid"),
            InlineKeyboardButton.WithCallbackData("БалышевЭкспрес", "business")
            }
        });
        await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите категорию:", replyMarkup: keyboard);
        return;
    }

    await botClient.SendTextMessageAsync(message.Chat.Id, $"Вы написали:\n{message.Text}");


}
async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{

    if (callbackQuery.Data.StartsWith("economy"))
    {
        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Выбран эконом класс");

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

