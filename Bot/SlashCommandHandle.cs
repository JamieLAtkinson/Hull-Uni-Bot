﻿using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Interactions;
namespace Bot;

public class SlashCommandHandle
{
    private readonly SocketGuild _Guild;
    
    public SlashCommandHandle(SocketGuild GetGuild)
    {
        _Guild = GetGuild;
    }

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
    
    
    private async Task RoleCommand(ISlashCommandInteraction Interaction)
    {
        try
        {
            SocketGuildUser User = (SocketGuildUser)Interaction.User;

            string RoleName = (string)Interaction.Data.Options.First().Value;
            uint HexCode;

            // Validate HexCode input
            if (!uint.TryParse((string?)Interaction.Data.Options.ElementAt(1).Value, out HexCode))
            {
                await Interaction.RespondAsync("Invalid hex code provided. Please enter a valid 6-digit hexadecimal value.");
                return;
            }

            var Color = new Color(HexCode);

            var Role = await _Guild.CreateRoleAsync(RoleName, null, Color);

            await User.AddRoleAsync(Role);

            await Interaction.RespondAsync($"**Role Name:** {RoleName} \n**Hex Code:** {HexCode}");
        }
        catch (Exception ex)
        {
            await Interaction.RespondAsync($"An error occurred: {ex.Message}");
        }
    }
}