using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection().AddHttpClient();
            var bus = CqrsBus
                .UseAssemblyFor<Program>(services)
                .Build();

            if (args.Length != 2)
                System.Console.WriteLine("You must provide a command (get)");

            if (args[0].ToLowerInvariant() == "get")
            {
                var content = bus.ExecuteAsync<string, string>(args[1]).Result;
                System.Console.WriteLine(content);
            }
        }
    }
}