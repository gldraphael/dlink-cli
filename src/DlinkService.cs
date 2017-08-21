using System;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Dlink.Cli.Models;

namespace Dlink.Cli
{
    public class DlinkService : IDisposable
    {
        private HttpClient http;
        private const string ip = "192.168.0.1";

        public DlinkService()
        {
            http = new HttpClient();
        }

        public async Task<bool> DhcpReleaseAsync()
        {
            var response = await http.GetAsync($"http://{ip}/Status/wan_button_action.asp?connect=false");
            if(!response.IsSuccessStatusCode) return false;
            return (await response.Content.ReadAsStringAsync()).Contains("Done");
        }

        public async Task<bool> DhcpRenewAsync()
        {
            var response = await http.GetAsync($"http://{ip}/Status/wan_button_action.asp?connect=true");
            if(!response.IsSuccessStatusCode) return false;
            return (await response.Content.ReadAsStringAsync()).Contains("Done");
        }

        public async Task<Status> GetStatusAsync()
        {
            var response = await http.GetAsync($"http://{ip}/Status/wan_connection_status.asp");
            if(!response.IsSuccessStatusCode)
            {
                return null;
            }

            DataContractSerializer serializer = new DataContractSerializer(typeof(Status));
            var reader = XmlDictionaryReader.CreateTextReader(await response.Content.ReadAsStreamAsync(), new XmlDictionaryReaderQuotas());
            Status status = serializer.ReadObject(reader, true) as Status;
            reader.Close();
            return status;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    http.Dispose();
                    http = null;
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}