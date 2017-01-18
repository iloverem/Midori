# Midori
Midori is an open-source .NET Core C# bot for Discord.  

# Installation  
* MIDORI REQUIRES THE FOLLOWING **USER** ENVIRONMENT VARIABLES:
* Midori_AcceptBotCommands (either false or true), whether to accept commands from bots. (Recommended false)
* Midori_AlertOnUnknownCommand (either false or true), whether to show an error if the supplied command doesn't exist. (Recommended false)
* Midori_BotDescription, set to your bot description (Shown above help)
* Midori_ConnectionToken, set to your Bot User Token
* Install .NET Core so you can run it from the Command Prompt (ex. `dotnet -h`)  
* Download the latest version from Releases  
* Open a command prompt where `MidoriBot.dll` is  
* Run `dotnet MidoriBot.dll`.
  
# Building
You need: 
* VS 2015 / MSBuild
* .NET Core

Build instructions coming soon.

# Roadmap
1.1: More commands.  
1.2: Standalone `Midori.exe` instead of a `dotnet MidoriBot.dll`. JSON configuration.
