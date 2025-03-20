using System.Text;

using Spectre.Console;

internal class Program {
    
    static void Main(string[] args) {
        
        Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;
        DateTime? procDate = null;

        while (procDate == null) {
            Console.Clear();
            DateTime curTime = DateTime.Now;
            Console.WriteLine($"✨ {curTime:yy}{curTime.DayOfYear:000}{(char)(curTime.Hour + 'a')}{curTime:mm} -- ✨⛔📘🎲📞💬✔️❌");

            try {
                Console.Write("Введіть дату: ");
                string? userInput = Console.ReadLine();
                procDate = DateTime.ParseExact(userInput, ["yyyy-MM-dd"], null);

                Console.WriteLine($"{procDate:yy}{procDate?.DayOfYear:000}a 🆔");
                procDate = null;
            }
            catch {
                Console.WriteLine("Помилка вводу!");
            }
        }
    }
}