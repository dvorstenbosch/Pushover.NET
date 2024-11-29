using PushoverClient;
using System.CommandLine;
using System.CommandLine.Help;

Option<string> titleOption = new Option<string>(name: "--title", description: "The message title");
titleOption.AddAlias("-t");
Option<string> messageOption = new Option<string>(name: "--message", description: "The message");
messageOption.AddAlias("-m");
Option<string> fromOption = new Option<string>(name: "--from", description: "The Pushover api key to send the message from");
fromOption.AddAlias("-f");
Option<string> userOption = new Option<string>(name: "--user", description: "The Pushover api key to send the message to");
userOption.AddAlias("-u");

RootCommand rootCommand = new RootCommand("Send a message using the PushOver API");
rootCommand.AddOption(titleOption);
rootCommand.AddOption(messageOption);
rootCommand.AddOption(fromOption);
rootCommand.AddOption(userOption);

rootCommand.SetHandler(SendMessageAsync, titleOption, messageOption, fromOption, userOption);

await rootCommand.InvokeAsync(args);

async Task SendMessageAsync(string title, string message, string from, string user)
{
    if (OptionsValid(title, message, from, user))
    {
        //  Send the message
        Pushover pclient = new Pushover(from);
        PushResponse response = pclient.Push(title, message, user);
    }
    else
    {
        HelpBuilder helpBuilder = new HelpBuilder(LocalizationResources.Instance);
        helpBuilder.Write(new HelpContext(helpBuilder, rootCommand, Console.Out));
    }
    await Task.Delay(1000);
}

static bool OptionsValid(string title, string message, string from, string user)
{
    bool valid = false;

    if (!string.IsNullOrEmpty(from) &&
        !string.IsNullOrEmpty(user) &&
        !string.IsNullOrEmpty(title) &&
        !string.IsNullOrEmpty(message))
    {
        valid = true;
    }

    return valid;
}