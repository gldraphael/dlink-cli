namespace Dlink.Cli.Models
{
    public class Status
    {
        public string WanIpAddress { get; set; }
        public string WanSubnet { get; set; }
        public string WanGateway { get; set; }
        public string WanPrimaryDns { get; set; }
        public string WanSecondaryDns { get; set; }
        public string WanInterfaceUptime { get; set; }
        
    }
}