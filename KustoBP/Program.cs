using Spectre.Console;
using System.Globalization;
using System.Text;

internal class Program {
    
    static void Main(string[] args) {
        Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;
        NTopMenu topMenuChoice;

        do {
            topMenuChoice = ShowMenu<NTopMenu>();
            AnsiConsole.MarkupLine($"Параметри ідентифікатору для: {NKey.Title.GetTag(topMenuChoice)}");

            string? displayName =   PromptText("Відображувана назва:");
            string? codeName =      PromptText("       Name in code:");
            string? abbr = NKey.Abbr.GetTag(topMenuChoice);
            string? sign = NKey.Sign.GetTag(topMenuChoice);
            string timeStamp = (PromptDate($"    Мітка часу:", "yyyy-MM-dd", "yyyy-MM-dd HH-mm") ?? DateTime.Now).AsShortTimeCode();

            AnsiConsole.MarkupLine($"\n[steelblue]{abbr}{timeStamp}[/] {sign} {displayName}\n[palegreen3]{abbr}{timeStamp}__{codeName}[/]\n");

        } while (topMenuChoice != NTopMenu.Exit);
    }



    private static String? PromptText(string prompt) 
        => AnsiConsole.Prompt(new TextPrompt<string?>(prompt).AllowEmpty().DefaultValue(null).HideDefaultValue().PromptStyle("green"));

    private static DateTime? PromptDate(string prompt, params string[] formats) {
        DateTime userDate = DateTime.Now;
        formats ??= ["yyyy-MM-dd", "yyyy-MM-dd HH-mm"];

        var input = AnsiConsole.Prompt(
            new TextPrompt<string?>(prompt)
                .AllowEmpty().DefaultValue(null).HideDefaultValue().PromptStyle("green")
                .Validate(txt => {
                    return DateTime.TryParseExact(
                        txt, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out userDate)
                        ? ValidationResult.Success()
                        : ValidationResult.Error($"[red]Тільки час у форматі {String.Join(", ", formats.Select(e => $"[olive]{e}[/]"))}[/]");
                })
        );
        return userDate;
    }
    

    private static T ShowMenu<T>() where T : struct, Enum {
        Dictionary<string, T> menuItemsAndKeys = NKey.Title.GetTags<T>();

        var userChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Для чого створити ідентифікатор?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more items)[/]")
                .AddChoices(menuItemsAndKeys.Keys));
        return menuItemsAndKeys[userChoice];
    }
}




public enum NTopMenu {
    [Tag(NKey.Sign, "🎲")]
    [Tag(NKey.Abbr, "N")]
    [Tag(NKey.Title, "🎲 Перелік (e[underline]N[/]um)")]
    Enum,
    [Tag(NKey.Sign, "📘")]
    [Tag(NKey.Abbr, "E")]
    [Tag(NKey.Title, "📘 Довідник ([underline]E[/]ntity)")]
    Entity,
    [Tag(NKey.Sign, "📜")]
    [Tag(NKey.Abbr, "D")]
    [Tag(NKey.Title, "📜 Документ ([underline]D[/]ocument)")]
    Document,
    [Tag(NKey.Sign, "🛠️")]
    [Tag(NKey.Abbr, "PC")]
    [Tag(NKey.Title, "🛠️ Налаштування процесу ([underline]P[/]rocess[underline]C[/]onfig)")]
    ProcConfig,
    [Tag(NKey.Sign, "⚙️")]
    [Tag(NKey.Abbr, "P")]
    [Tag(NKey.Title, "⚙️ Процес ([underline]P[/]rocess)")]
    Process,
    [Tag(NKey.Title, "❌ [red]ВИХІД[/])")]
    Exit
}

public static class DateTime__Ext {
    public static String AsShortTimeCode(this DateTime timeStamp)
        => $"{timeStamp:yy}{timeStamp.DayOfYear:000}{(char)('a' + timeStamp.Hour)}{timeStamp:mm}";
}

public enum NKey {
    Title,
    Sign,
    Abbr
}



[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class TagAttribute : Attribute {
    public string Name { get; }
    public NKey Key { get; }

    public TagAttribute(NKey key, string name) {
        Name = name;
        Key = key;
    }
}

public static class EnumExtensions {
    public static string? GetTag<T>(this NKey key, T enumValue) where T : struct, Enum {
        var type = enumValue.GetType();
        var memberInfo = type.GetMember(enumValue.ToString());
        if (memberInfo.Length == 0) return null;
        var attribute = memberInfo[0].GetCustomAttributes(typeof(TagAttribute), false).Cast<TagAttribute>().FirstOrDefault(e => e.Key == key);
        return attribute?.Key == key ? attribute.Name : null;
    }

    public static Dictionary<String, T> GetTags<T>(this NKey key) where T : struct, Enum {
        return Enum.GetValues<T>().ToDictionary(k => key.GetTag(k)!);
    }
}
