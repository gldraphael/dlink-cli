using System;
using System.Net.Http;
using System.Threading.Tasks;
using C = FluentColorConsole.ColorConsole;

namespace Dlink.Cli
{
    class Program
    {   
        static async Task Main(string[] args)
        {
            try
            {
                C.WithBlueText.WriteLine("Unofficial dlink CLI v0.1");
                await parseArgumentsAndRun(args);
            }
            catch(Exception e)
            {
                C.WithDarkRedText.WriteLine("Something's gone wrong!");
                C.WithDarkGrayText.WriteLine(e.Message);
                C.WithDarkGrayText.WriteLine(e.StackTrace);
            }
        }

        static async Task<bool> parseArgumentsAndRun(string[] args)
        {
            if(args == null || args.Length == 0 || args[0].Is("help"))
            {
                printHelp();
                return true;
            }

            var option = args[0];
            var arg = args.Length > 1 ? args[1] : null;
            var dlink = new DlinkService();

            if(option.Is("release"))
            {
                Console.WriteLine($"Performing a DHCP release");
                if(!await dlink.DhcpReleaseAsync())
                {
                    C.WithDarkRedText.WriteLine("DHCP release failed.");
                    return false;
                }
            }

            else if(option.Is("renew"))
            {
                Console.WriteLine($"Performing a DHCP renew");
                C.WithDarkGrayText.WriteLine($"Please wait...");
                if(!await dlink.DhcpRenewAsync())
                {
                    C.WithDarkRedText.WriteLine("DHCP renew failed.");
                    return false;
                }
                Console.WriteLine($"Your WAN IP is: {await dlink.GetWanIpAddressAsync() ?? "ERROR"}");
            }

            else if(option.Is("refresh"))
            {
                Console.WriteLine($"Releasing the DHCP lease");
                if(!await dlink.DhcpReleaseAsync())
                {
                    C.WithDarkRedText.WriteLine("DHCP release failed.");
                    return false;
                }

                Console.WriteLine($"Renewing the DHCP connection");
                C.WithDarkGrayText.WriteLine($"Please wait...");
                if(!await dlink.DhcpRenewAsync())
                {
                    C.WithDarkRedText.WriteLine("DHCP renew failed.");
                    return false;
                }

                Console.WriteLine($"Your WAN IP is: {await dlink.GetWanIpAddressAsync() ?? "ERROR"}");

                return true;
            }

            else if(option.Is("status"))
            {
                var status = await dlink.GetStatusAsync();
                if(status == null) 
                {
                    C.WithDarkRedText.WriteLine("Error while fetching status");
                    return false;
                }

                Console.WriteLine($"WAN IP Address:   {status.WanIpAddress}");
                Console.WriteLine($"WAN Subnet:       {status.WanSubnet}");
                Console.WriteLine($"WAN Gateway:      {status.WanGateway}");
                Console.WriteLine($"Primary DNS:      {status.WanPrimaryDns}");
                Console.WriteLine($"Secondary DNS:    {status.WanSecondaryDns}");
                Console.WriteLine($"Interface Uptime: {status.WanInterfaceUptime}");
                return true;
            }

            else if(option.Is("ip"))
            {
                var ip = await dlink.GetWanIpAddressAsync();
                if(ip == null) 
                {
                    C.WithDarkRedText.WriteLine("Error while fetching status");
                    return false;
                }

                Console.WriteLine($"WAN IP: {ip}");
                return true;
            }
            
            C.WithDarkRedText.WriteLine("That's an invalid option.");
            printHelp();
            return false;
        }

        static void printHelp() 
        {
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine(" dlink <status|release|renew|refresh|ip>");
            Console.WriteLine();
            Console.WriteLine(" --status   - Prints connection status information.");
            Console.WriteLine(" --release  - Releases the DHCP lease.");
            Console.WriteLine(" --renew    - Renews the DHCP connection.");
            Console.WriteLine(" --refresh  - Performs a DHCP release and renew");
            Console.WriteLine(" --ip       - Prints the WAN IP Address");
        }
    }
}
