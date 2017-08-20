using System;
using System.Net.Http;
using System.Threading.Tasks;
using C = FluentColorConsole.ColorConsole;

namespace Dlink.Cli
{
    class Program
    {

        private const string ip = "192.168.0.1";
        
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
                using(HttpClient http = new HttpClient())
                {
                    Console.WriteLine($"Performing a DHCP release");
                    var response = await http.GetAsync($"http://{ip}/Status/wan_button_action.asp?connect=false");
                    if(!response.IsSuccessStatusCode) return false;
                    return (await response.Content.ReadAsStringAsync()).Contains("Done");
                }
            }

            else if(option.Is("renew"))
            {
                using(HttpClient http = new HttpClient())
                {
                    Console.WriteLine($"Performing a DHCP renew");
                    var response = await http.GetAsync($"http://{ip}/Status/wan_button_action.asp?connect=true");
                    if(!response.IsSuccessStatusCode) return false;
                    return (await response.Content.ReadAsStringAsync()).Contains("Done");
                }
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
