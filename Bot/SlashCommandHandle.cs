﻿using Discord;
using Discord.WebSocket;
using Discord.Commands;
namespace Bot;

public class SlashCommandHandle
{
    public Task SlashCommandExecuted(SocketSlashCommand Interaction)
    {
        return Interaction.Data.Name switch
        {
            "ping" => PingCommand(Interaction),
            "role" => RoleCommand(Interaction),
            _ => Task.CompletedTask
        };
    }

// ~~ ALL SLASH COMMAND EXECUTION ~~

    private Task PingCommand(ISlashCommandInteraction Interaction)
    {
        return Interaction.RespondAsync("Pong!");
    }

    private Task RoleCommand(ISlashCommandInteraction Interaction)
    {
        var RoleName = Interaction.Data.Options.First().Value;
        var HexCode = Interaction.Data.Options.ElementAt(1).Value;
        
        return Interaction.RespondAsync($"**Role Name:** {RoleName} \n**Hex Code:** {HexCode}");
    }
}