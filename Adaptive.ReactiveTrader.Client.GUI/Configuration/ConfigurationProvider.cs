using System.Configuration;

namespace NewWave.FastTrader.Client.Configuration
{
    class ConfigurationProvider : IConfigurationProvider
    {
        public string[] Servers
        {
            get
            {
                var servers = ConfigurationManager.AppSettings["servers"];
                if (string.IsNullOrEmpty(servers))
                {
                    throw new ConfigurationErrorsException("AppSettings 'servers' key is not defined or empty.");
                }

                return servers.Split(';');
            }
        }
    }
}