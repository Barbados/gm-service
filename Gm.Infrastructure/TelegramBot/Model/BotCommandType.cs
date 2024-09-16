using Ardalis.SmartEnum;

namespace Gm.Infrastructure.TelegramBot.Model;

[SmartEnumStringComparer(StringComparison.InvariantCultureIgnoreCase)]
public sealed class BotCommandType : SmartEnum<BotCommandType, string>
{
    public static readonly BotCommandType None = new(nameof(None), string.Empty);
    public static readonly BotCommandType Start = new(nameof(Start), "/start");
    public static readonly BotCommandType Register = new(nameof(Register), "/register");

    private BotCommandType(string name, string value) : base(name, value)
    {
    }
}