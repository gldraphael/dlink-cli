using System;
using System.Threading.Tasks;
using C = FluentColorConsole.ColorConsole;

namespace Dlink.Cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            C.WithBlueText.WriteLine("Unofficial dlink CLI v0.1");
            var result = await parseArgumentsAndGetTask(args);
            if(result)
            {
                Console.WriteLine("Done.");
            }
        }

        static async Task<bool> parseArgumentsAndGetTask(string[] args)
        {
            if(args == null || args.Length == 0 || args[0].Is("help"))
            {
                printHelp();
                return true;
            }

            var option = args[0];
            var arg = args.Length > 1 ? args[1] : null;

            if(option.Is("release"))
            {
                Console.WriteLine($"Performing a DHCP release");
                return await Task.FromResult(true);
            }

            else if(option.Is("renew"))
            {
                Console.WriteLine($"Performing a DHCP renew");
                return await Task.FromResult(true);
            }
            
            return false;
        }

        static void printHelp() 
        {
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine(" dlink <release|renew>");
            Console.WriteLine();
            Console.WriteLine(" --release  - Performs a DHCP release");
            Console.WriteLine(" --renew    - Performs a DHCP renew");
        }
    }
}
