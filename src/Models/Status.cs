using System.Runtime.Serialization;

namespace Dlink.Cli.Models
{
    [DataContract(Name = "status", Namespace = "")]
    public class Status
    {
        [DataMember(Name = "wan_ip_address")]
        public string WanIpAddress { get; set; }

        [DataMember(Name = "wan_subnet")]
        public string WanSubnet { get; set; }

        [DataMember(Name = "wan_gateway")]
        public string WanGateway { get; set; }

        [DataMember(Name = "wan_primary_dns")]
        public string WanPrimaryDns { get; set; }

        [DataMember(Name = "wan_secondary_dns")]
        public string WanSecondaryDns { get; set; }

        [DataMember(Name = "wan_interface_uptime")]
        public string WanInterfaceUptime { get; set; }
    }
}