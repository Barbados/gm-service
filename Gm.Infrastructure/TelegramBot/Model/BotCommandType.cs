using Ardalis.SmartEnum;

namespace Gm.Infrastructure.TelegramBot.Model;

[SmartEnumStringComparer(StringComparison.InvariantCultureIgnoreCase)]
public sealed class BotCommandType : SmartEnum<BotCommandType, string>
{
    public static readonly BotCommandType None = new(nameof(None), string.Empty);
    public static readonly BotCommandType Start = new(nameof(Start), "/start");
    public static readonly BotCommandType Subscribe = new(nameof(Subscribe), "/subscribe");
    public static readonly BotCommandType Unsubscribe = new(nameof(Unsubscribe), "/unsubscribe");
    public static readonly BotCommandType Test = new(nameof(Test), "/test");

    private BotCommandType(string name, string value) : base(name, value)
    {
    }
}