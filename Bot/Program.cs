﻿


using Discord;
using Discord.WebSocket;

public class Program
{
    public static Task Main(string[] args) => new Program().MainAsync();
    
    private DiscordSocketClient _client;

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();

        _client.Log += Log;
        
        var token = Environment.GetEnvironmentVariable("BotToken");;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        client.Ready += Client_Ready;

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }
    
    private Task Log(LogMessage Msg)
    {
        Console.WriteLine(Msg.ToString());
        return Task.CompletedTask;
    }
}